using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.VideoConverterService.Abstractions;
using TokanPages.Services.VideoConverterService.Models;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.VideoProcessingService.Models;
using TokanPages.Services.VideoProcessingService.Utilities;
using Microsoft.EntityFrameworkCore;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Services.VideoProcessingService;

public class VideoProcessor : IVideoProcessor
{
    private readonly IVideoConverter _videoConverter;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly DatabaseContext _databaseContext;

    public VideoProcessor(DatabaseContext databaseContext, IDateTimeService dateTimeService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IVideoConverter videoConverter, 
        ILoggerService loggerService)
    {
        _databaseContext = databaseContext;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _videoConverter = videoConverter;
        _loggerService = loggerService;
    }

    public async Task Process(RequestVideoProcessing request)
    {
        _loggerService.LogInformation("Getting video info from database...");
        var uploadedVideo = await GetUploadedVideoInfo(request.TicketId);
        uploadedVideo.Status = VideoStatus.ProcessingStarted;
        await _databaseContext.SaveChangesAsync();

        _loggerService.LogInformation("Getting video file from Azure Storage...");
        var azureBlob = _azureBlobStorageFactory.Create(_loggerService);
        var tempVideoFile = await azureBlob.OpenRead(uploadedVideo.SourceBlobUri);

        _loggerService.LogInformation("Moving video file into memory stream...");
        using var memoryStream = new MemoryStream();
        await TempVideoFileValidation(tempVideoFile, uploadedVideo);
        await tempVideoFile!.Content!.CopyToAsync(memoryStream);

        _loggerService.LogInformation("Moving stream content into byte array...");
        var bytes = memoryStream.ToArray();
        var tempVideoName = $"{Guid.NewGuid():N}.mp4".ToLower();

        _loggerService.LogInformation("Begin converting video file...");
        var shouldCompactVideo = request.Details?.ShouldCompactVideo ?? false;
        var converterOutput = await _videoConverter.Convert(bytes, tempVideoName, shouldCompactVideo);

        _loggerService.LogInformation("Video converted, thumbnail generated...");
        _loggerService.LogInformation("Uploading files to Azure Cloud...");

        await UploadAndDelete(request, converterOutput, uploadedVideo, azureBlob);
        _loggerService.LogInformation("Data uploaded, temporary files removed");

        var hasProcessingWarning = !string.IsNullOrEmpty(converterOutput.ProcessingWarning);
        uploadedVideo.Status = hasProcessingWarning 
            ? VideoStatus.ProcessingFinishedWithWarnings 
            : VideoStatus.ProcessingFinished;
        uploadedVideo.ModifiedAt = _dateTimeService.Now;
        uploadedVideo.ModifiedBy = request.UserId;
        uploadedVideo.IsSourceDeleted = true;
        uploadedVideo.ProcessingWarning = converterOutput.ProcessingWarning;
        uploadedVideo.InputSizeInBytes = converterOutput.InputSizeInBytes;
        uploadedVideo.OutputSizeInBytes = converterOutput.OutputSizeInBytes;

        await _databaseContext.SaveChangesAsync();
        _loggerService.LogInformation("Data processed and saved!");
    }

    private async Task<UploadedVideo> GetUploadedVideoInfo(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var uploadedVideo = await _databaseContext.UploadedVideos
            .Where(uploadedVideos => uploadedVideos.TicketId == ticketId)
            .Where(uploadedVideos => uploadedVideos.Status == VideoStatus.New)
            .SingleOrDefaultAsync(cancellationToken);

        if (uploadedVideo is null)
            throw new GeneralException(Errors.ErrorNoVideo);

        return uploadedVideo;
    }

    private async Task TempVideoFileValidation(StorageStreamContent? content, UploadedVideo uploadedVideo, CancellationToken cancellationToken = default)
    {
        _loggerService.LogInformation("Validating temporary video file content...");

        if (content?.Content is null || content.ContentType is null)
        {
            uploadedVideo.Status = VideoStatus.Failed;
            await _databaseContext.SaveChangesAsync(cancellationToken);
            _loggerService.LogError(Errors.ErrorNoStreamContent);
            throw new GeneralException(Errors.ErrorNoStreamContent);
        }
    }

    private static async Task UploadAndDelete(RequestVideoProcessing request, ConverterOutput converterOutput, UploadedVideo uploadedVideo, IAzureBlobStorage azureBlob)
    {
        var basePath = "content/uncategorized/";
        var basePathAssets = "content/assets/";

        basePath = request.Details?.Target switch
        {
            ProcessingTarget.PresentationVideo => basePathAssets,
            ProcessingTarget.LearningVideo => basePathAssets,
            _ => basePath
        };

        var videoToUpload = await File.ReadAllBytesAsync(converterOutput.OutputVideoPath);
        using var videoStream = new MemoryStream(videoToUpload);
        await azureBlob.UploadFile(videoStream, $"{basePath}{uploadedVideo.TargetVideoUri}", "video/mp4");
        File.Delete(converterOutput.OutputVideoPath);

        var thumbnailToUpload = await File.ReadAllBytesAsync(converterOutput.OutputThumbnailPath);
        using var thumbnailStream = new MemoryStream(thumbnailToUpload);
        await azureBlob.UploadFile(thumbnailStream, $"{basePath}{uploadedVideo.TargetThumbnailUri}", "image/jpeg");
        File.Delete(converterOutput.OutputThumbnailPath);

        await azureBlob.DeleteFile(uploadedVideo.SourceBlobUri);
    }
}