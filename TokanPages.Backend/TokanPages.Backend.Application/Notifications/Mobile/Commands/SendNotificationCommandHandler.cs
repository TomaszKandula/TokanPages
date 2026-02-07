using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Models;
using Microsoft.Azure.NotificationHubs;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Notification;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class SendNotificationCommandHandler : RequestHandler<SendNotificationCommand, SendNotificationCommandResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    private readonly IAzureNotificationHubUtility _azureNotificationHubUtility;

    private readonly INotificationRepository _notificationRepository;

    public SendNotificationCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory, IAzureNotificationHubUtility azureNotificationHubUtility, 
        INotificationRepository notificationRepository) : base(operationDbContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
        _azureNotificationHubUtility = azureNotificationHubUtility;
        _notificationRepository = notificationRepository;
    }

    public override async Task<SendNotificationCommandResult> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var payload = GetPayload(request);
        var result = await ExecutePushNotification(request.Platform, payload, request.Tags, cancellationToken);
        var affected = new List<RegistrationData>();

        if (result.Results is not null)
        {
            if (result.Results.Count == 0)
                return new SendNotificationCommandResult
                {
                    Succeeded = result.Success,
                    Failed = result.Failure,
                    AffectedRegistrations = affected
                };

            var logs = new List<PushNotificationLogDto>();
            foreach (var item in result.Results)
            {
                affected.Add(new RegistrationData
                {
                    RegistrationId = item.RegistrationId,
                    PnsHandle = item.PnsHandle,
                    ApplicationPlatform = item.ApplicationPlatform
                });

                logs.Add(new PushNotificationLogDto
                {
                    RegistrationId = item.RegistrationId,
                    Handle = item.PnsHandle,
                    Platform = item.ApplicationPlatform,
                    Payload = payload
                });                
            }

            if (logs.Count == 0)
                return new SendNotificationCommandResult
                {
                    Succeeded = result.Success,
                    Failed = result.Failure,
                    AffectedRegistrations = affected
                };

            await _notificationRepository.CreatePushNotificationLogs(logs);
        }

        return new SendNotificationCommandResult
        {
            Succeeded = result.Success,
            Failed = result.Failure,
            AffectedRegistrations = affected
        };
    }

    private string GetPayload(SendNotificationCommand request)
    {
        var input = new MessageInput
        {
            Title = request.MessageTitle,
            Body = request.MessageBody
        };

        switch (request.Platform)
        {
            case PlatformType.Aps:
                return _azureNotificationHubUtility.MakeApsMessage(input);
            case PlatformType.Gcm:
            case PlatformType.Fcm:
                return _azureNotificationHubUtility.MakeFcmMessage(input);
            default:
                throw new BusinessException(nameof(ErrorCodes.UNSUPPORTED_PLATFORM), ErrorCodes.UNSUPPORTED_PLATFORM);                
        }
    }

    private async Task<NotificationOutcome> ExecutePushNotification(PlatformType platform, string payload, string[]? tags, CancellationToken cancellationToken = default)
    {
        var hub = _azureNotificationHubFactory.Create();
        if (tags is null)
            return await hub.SendPushNotification(platform, payload, cancellationToken);

        return await hub.SendPushNotification(platform, payload, tags, cancellationToken);
    }
}