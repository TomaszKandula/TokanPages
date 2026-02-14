using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Services.VideoConverterService.Abstractions;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.VideoProcessingService.Models;
using TokanPages.Services.VideoProcessingService.Utilities;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Content;
using TokanPages.Persistence.DataAccess.Repositories.Content.Models;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Services.VideoProcessingService;

[ExcludeFromCodeCoverage]
internal sealed class VideoProcessor : IVideoProcessor
{
    private readonly IVideoConverter _videoConverter;

    private readonly ILoggerService _loggerService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IContentRepository _contentRepository;

    public VideoProcessor(IAzureBlobStorageFactory azureBlobStorageFactory, IVideoConverter videoConverter, 
        ILoggerService loggerService, IContentRepository contentRepository)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _videoConverter = videoConverter;
        _loggerService = loggerService;
        _contentRepository = contentRepository;
    }

    public async Task Process(RequestVideoProcessing request)
    {
        _loggerService.LogInformation("Getting video info from database...");

        var uploadedVideo = await _contentRepository.GetVideoUploadStatus(request.TicketId, VideoStatus.New);
        if (uploadedVideo is null)
            throw new GeneralException(Errors.ErrorNoVideo);
        
        await _contentRepository.UpdateVideoUploadStatus(request.TicketId, VideoStatus.ProcessingStarted);

        _loggerService.LogInformation("Getting video file from Azure Storage...");
        var azureBlob = _azureBlobStorageFactory.Create(_loggerService);
        var tempVideoFile = await azureBlob.OpenRead(uploadedVideo.SourceBlobUri);

        _loggerService.LogInformation("Validating temporary video file content...");
        if (tempVideoFile?.Content is null || tempVideoFile.ContentType is null)
        {
            await _contentRepository.UpdateVideoUploadStatus(request.TicketId, VideoStatus.Failed);
            _loggerService.LogError(Errors.ErrorNoStreamContent);
            throw new GeneralException(Errors.ErrorNoStreamContent);
        }

        _loggerService.LogInformation("Moving video file into memory stream...");
        using var memoryStream = new MemoryStream();
        await tempVideoFile.Content.CopyToAsync(memoryStream);

        _loggerService.LogInformation("Moving stream content into byte array...");
        var bytes = memoryStream.ToArray();
        var tempVideoName = $"{Guid.NewGuid():N}.mp4".ToLower();

        _loggerService.LogInformation("Begin converting video file...");
        var shouldCompactVideo = request.Details?.ShouldCompactVideo ?? false;
        var converterOutput = await _videoConverter.Convert(bytes, tempVideoName, shouldCompactVideo);

        _loggerService.LogInformation("Video converted, thumbnail generated...");
        _loggerService.LogInformation("Uploading files to Azure Cloud...");

        var videoToUpload = await File.ReadAllBytesAsync(converterOutput.OutputVideoPath);
        using var videoStream = new MemoryStream(videoToUpload);
        await azureBlob.UploadFile(videoStream, $"{uploadedVideo.TargetVideoUri}", "video/mp4");
        File.Delete(converterOutput.OutputVideoPath);

        var thumbnailToUpload = await File.ReadAllBytesAsync(converterOutput.OutputThumbnailPath);
        using var thumbnailStream = new MemoryStream(thumbnailToUpload);
        await azureBlob.UploadFile(thumbnailStream, $"{uploadedVideo.TargetThumbnailUri}", "image/jpeg");
        File.Delete(converterOutput.OutputThumbnailPath);

        await azureBlob.DeleteFile(uploadedVideo.SourceBlobUri);
        _loggerService.LogInformation("Data uploaded, temporary files removed");

        var hasProcessingWarning = !string.IsNullOrEmpty(converterOutput.ProcessingWarning);
        var status = hasProcessingWarning 
            ? VideoStatus.ProcessingFinishedWithWarnings 
            : VideoStatus.ProcessingFinished;

        await _contentRepository.UpdateVideoUploadStatus(request.TicketId, status);

        var videoUpload = new UpdateVideoUploadDto
        {
            TicketId = request.TicketId,
            IsSourceDeleted = true,
            ProcessingWarning = converterOutput.ProcessingWarning,
            InputSizeInBytes = converterOutput.InputSizeInBytes,
            OutputSizeInBytes = converterOutput.OutputSizeInBytes
        };

        await _contentRepository.UpdateVideoUpload(videoUpload);
        _loggerService.LogInformation("Data processed and saved!");
    }
}