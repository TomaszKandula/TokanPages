using TokanPages.Backend.Application.Assets.Commands.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.AzureStorageService.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddVideoAssetCommandHandler : RequestHandler<AddVideoAssetCommand, AddVideoAssetCommandResult>
{
    private const string ContainerPresentations = "presentations";

    private const string ContainerLearnings = "learnings";

    private const string IdPresentations = "presentations-id";

    private const string IdLearnings = "learnings-id";

    private const string QueueName = "video_queue";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IAzureBusFactory _azureBusFactory;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public AddVideoAssetCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IAzureBusFactory azureBusFactory, 
        IJsonSerializer jsonSerializer, IDateTimeService dateTimeService, 
        IUserService userService) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _azureBusFactory = azureBusFactory;
        _jsonSerializer = jsonSerializer;
        _dateTimeService = dateTimeService;
        _userService = userService;
    }

    public override async Task<AddVideoAssetCommandResult> Handle(AddVideoAssetCommand request, CancellationToken cancellationToken)
    {
        if (request.BinaryData is not null && request.BinaryData.Length > 0)
            return await ProcessBinaryData(request, cancellationToken);

        throw new GeneralException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);
    }

    private static string GetContainer(bool hasPresentationId, bool hasLearningId)
    {
        if (hasPresentationId)
            return ContainerPresentations;

        return hasLearningId ? ContainerLearnings : string.Empty;
    }

    private async Task<AddVideoAssetCommandResult> ProcessBinaryData(AddVideoAssetCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var azureBlob = _azureBlobStorageFactory.Create();
        var binaryData = request.BinaryData;

        var presentationId = _userService.GetGuidFromHeader(IdPresentations);
        var learningId = _userService.GetGuidFromHeader(IdLearnings);

        var hasPresentationId = presentationId != null;
        var hasLearningId = learningId != null;
        var hasCompactVideo = _userService.GetCompactVideoFromHeader();

        var videoName = binaryData!.FileName;
        var contentType = request.BinaryData!.ContentType;
        var container = GetContainer(hasPresentationId, hasLearningId);

        var tempPathFile = $"content/assets/temp/{videoName}";
        var targetBasePath = $"videos/{container}";

        var fileGuid = $"{Guid.NewGuid():N}".ToLower();
        var targetVideoUri = $"{targetBasePath}/{fileGuid}.mp4";
        var targetThumbnailUri = $"{targetBasePath}/{fileGuid}.jpg";

        var ticketId = Guid.NewGuid();
        var upload = new UploadedVideo
        {
            TicketId = ticketId,
            SourceBlobUri = tempPathFile,
            TargetVideoUri = targetVideoUri,
            TargetThumbnailUri = targetThumbnailUri,
            Status = VideoStatus.New,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = user.Id,
            IsSourceDeleted = false
        };

        var buffer = binaryData.GetByteArray();
        using var stream = new MemoryStream(buffer);

        await azureBlob.UploadFile(stream, tempPathFile, contentType, cancellationToken);
        await DatabaseContext.UploadedVideos.AddAsync(upload, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation($"New video has been uploaded for processing. Ticket ID: {ticketId}.");

        var details = container switch
        {
            ContainerPresentations => new TargetDetails
            {
                Target = ProcessingTarget.PresentationVideo,
                EntityId = presentationId,
                ShouldCompactVideo = hasCompactVideo
            },
            ContainerLearnings => new TargetDetails 
            {
                Target = ProcessingTarget.LearningVideo,
                EntityId = learningId, 
                ShouldCompactVideo = hasCompactVideo
            },
            _ => new TargetDetails()
        };

        await RequestVideoProcessing(ticketId, user.Id, details, cancellationToken);
        return new AddVideoAssetCommandResult { TicketId = ticketId };
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