using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Application.Content.Assets.Commands.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommandHandler : RequestHandler<AddUserFileCommand, AddUserFileCommandResult>
{
    private const string QueueName = "video_queue";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IAzureBusFactory _azureBusFactory;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public AddUserFileCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService, IAzureBusFactory azureBusFactory, 
        IJsonSerializer jsonSerializer, IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
        _azureBusFactory = azureBusFactory;
        _jsonSerializer = jsonSerializer;
        _dateTimeService = dateTimeService;
    }

    public override async Task<AddUserFileCommandResult> Handle(AddUserFileCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var basePath = $"content/users/{userId}/files";

        var binaryData = request.BinaryData;
        var contentType = request.BinaryData!.ContentType;

        Guid? ticketId = null;
        var isBeingProcessed = false;
        string prefixPath;

        switch (request.Type)
        {
            case UserFileToUpdate.Video:
                prefixPath = $"{basePath}/videos";
                ticketId = await AddVideoFile(binaryData!, contentType, prefixPath, userId, cancellationToken);
                isBeingProcessed = true;
                break;
            case UserFileToUpdate.Image:
                prefixPath = $"{basePath}/images";
                await AddNonVideoFile(binaryData!, contentType, prefixPath, cancellationToken);
                break;
            case UserFileToUpdate.Audio:
                prefixPath = $"{basePath}/audio";
                await AddNonVideoFile(binaryData!, contentType, prefixPath, cancellationToken);
                break;
            case UserFileToUpdate.Document:
                prefixPath = $"{basePath}/documents";
                await AddNonVideoFile(binaryData!, contentType, prefixPath, cancellationToken);
                break;
            case UserFileToUpdate.Application:
                prefixPath = $"{basePath}/applications";
                await AddNonVideoFile(binaryData!, contentType, prefixPath, cancellationToken);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new AddUserFileCommandResult
        {
            TicketId = ticketId,
            IsBeingProcessed = isBeingProcessed
        };
    }

    private async Task AddNonVideoFile(IFormFile binaryData, string contentType, string prefix, CancellationToken cancellationToken)
    {
        var fileName = binaryData.FileName;
        var binary = binaryData.GetByteArray();

        var extension = Path.GetExtension(fileName);
        var mediaName = $"{Guid.NewGuid():N}{extension}".ToLower();

        var destinationPath = $"{prefix}/{mediaName}";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New user file has been saved. Path: {destinationPath}.");
    }

    private async Task<Guid> AddVideoFile(IFormFile binaryData, string contentType, string prefix, Guid userId, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var hasCompactVideo = _userService.GetCompactVideoFromHeader();
        var videoName = binaryData.FileName;
        
        var tempPathFile = $"content/assets/temp/{videoName}";

        var fileGuid = $"{Guid.NewGuid():N}".ToLower();
        var targetVideoUri = $"{prefix}/{fileGuid}.mp4";
        var targetThumbnailUri = $"{prefix}/{fileGuid}.jpg";

        var ticketId = Guid.NewGuid();
        var upload = new UploadedVideo
        {
            TicketId = ticketId,
            SourceBlobUri = tempPathFile,
            TargetVideoUri = targetVideoUri,
            TargetThumbnailUri = targetThumbnailUri,
            Status = VideoStatus.New,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId,
            IsSourceDeleted = false
        };

        var buffer = binaryData.GetByteArray();
        using var stream = new MemoryStream(buffer);

        await azureBlob.UploadFile(stream, tempPathFile, contentType, cancellationToken);
        await DatabaseContext.UploadedVideos.AddAsync(upload, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation($"New user video has been uploaded for processing. Ticket ID: {ticketId}.");

        var details = new TargetDetails
        {
            //Target = request.Target,
            ShouldCompactVideo = hasCompactVideo
        };

        await RequestVideoProcessing(ticketId, userId, details, cancellationToken);
        return ticketId;
    }
    
    private async Task RequestVideoProcessing(Guid ticketId, Guid userId, TargetDetails details, CancellationToken cancellationToken = default)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        var requestBody = new RequestProcessing
        {
            MessageId = messageId,
            TicketId = ticketId,
            UserId = userId,
            TargetEnv = CurrentEnv,
            Details = details
        };

        await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var serialized = _jsonSerializer.Serialize(requestBody, Formatting.None, Settings);
        var messages = new List<string> { serialized };

        LoggerService.LogInformation($"[{QueueName} | {CurrentEnv}]: Send message ({messageId}) to service bus: '{serialized}'.");

        var busService = _azureBusFactory.Create();
        await busService.SendMessages(QueueName, messages, cancellationToken);
    }
}