using TokanPages.Backend.Application.Content.Assets.Commands.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.AzureStorageService.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Content;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;

namespace TokanPages.Backend.Application.Content.Assets.Commands;

public class AddVideoAssetCommandHandler : RequestHandler<AddVideoAssetCommand, AddVideoAssetCommandResult>
{
    private const string QueueName = "video_queue";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IAzureBusFactory _azureBusFactory;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IUserService _userService;

    private readonly IContentRepository _contentRepository;

    private readonly IMessagingRepository _messagingRepository;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public AddVideoAssetCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IAzureBusFactory azureBusFactory, IJsonSerializer jsonSerializer, 
        IUserService userService, IContentRepository contentRepository, IMessagingRepository messagingRepository) 
        : base(operationDbContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _azureBusFactory = azureBusFactory;
        _jsonSerializer = jsonSerializer;
        _userService = userService;
        _contentRepository = contentRepository;
        _messagingRepository = messagingRepository;
    }

    public override async Task<AddVideoAssetCommandResult> Handle(AddVideoAssetCommand request, CancellationToken cancellationToken)
    {
        if (request.BinaryData is not null && request.BinaryData.Length > 0)
            return await ProcessBinaryData(request, cancellationToken);

        throw new GeneralException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);
    }

    private async Task<AddVideoAssetCommandResult> ProcessBinaryData(AddVideoAssetCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var binaryData = request.BinaryData;

        var hasCompactVideo = _userService.GetCompactVideoFromHeader();

        var videoName = binaryData!.FileName;
        var contentType = request.BinaryData!.ContentType;

        var tempPathFile = $"content/assets/temp/{videoName}";
        var targetBasePath = $"videos/{request.Target}";

        var fileGuid = $"{Guid.NewGuid():N}".ToLower();
        var targetVideoUri = $"{targetBasePath}/{fileGuid}.mp4";
        var targetThumbnailUri = $"{targetBasePath}/{fileGuid}.jpg";

        var ticketId = Guid.NewGuid();
        var buffer = binaryData.GetByteArray();
        using var stream = new MemoryStream(buffer);
        await azureBlob.UploadFile(stream, tempPathFile, contentType, cancellationToken);
        await _contentRepository.CreateVideoUpload(userId, ticketId, tempPathFile, targetVideoUri, targetThumbnailUri);
        LoggerService.LogInformation($"New video has been uploaded for processing. Ticket ID: {ticketId}.");

        var details = new TargetDetails
        {
            Target = request.Target,
            ShouldCompactVideo = hasCompactVideo
        };

        await RequestVideoProcessing(ticketId, userId, details, cancellationToken);
        return new AddVideoAssetCommandResult { TicketId = ticketId };
    }

    private async Task RequestVideoProcessing(Guid ticketId, Guid userId, TargetDetails details, CancellationToken cancellationToken = default)
    {
        var messageId = Guid.NewGuid();
        var requestBody = new RequestProcessing
        {
            MessageId = messageId,
            TicketId = ticketId,
            UserId = userId,
            TargetEnv = CurrentEnv,
            Details = details
        };

        await _messagingRepository.CreateServiceBusMessage(messageId);
        var serialized = _jsonSerializer.Serialize(requestBody, Formatting.None, Settings);
        var messages = new List<string> { serialized };

        LoggerService.LogInformation($"[{QueueName} | {CurrentEnv}]: Send message ({messageId}) to service bus: '{serialized}'.");

        var busService = _azureBusFactory.Create();
        await busService.SendMessages(QueueName, messages, cancellationToken);
    }
}