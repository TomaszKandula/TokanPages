using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Notification;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Models;
using Microsoft.Azure.NotificationHubs;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class SendNotificationCommandHandler : RequestHandler<SendNotificationCommand, SendNotificationCommandResult>
{
    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    private readonly IAzureNotificationHubUtility _azureNotificationHubUtility;

    private readonly IDateTimeService _dateTimeService;
    
    public SendNotificationCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory, IAzureNotificationHubUtility azureNotificationHubUtility, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
        _azureNotificationHubUtility = azureNotificationHubUtility;
        _dateTimeService = dateTimeService;
    }

    public override async Task<SendNotificationCommandResult> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var createdAt = _dateTimeService.Now;
        var payload = GetPayload(request);
        var result = await ExecutePushNotification(request.Platform, payload, request.Tags, cancellationToken);
        var affected = new List<RegistrationData>();

        if (result.Results is not null)
        {
            if (!result.Results.Any())
                return new SendNotificationCommandResult
                {
                    Succeeded = result.Success,
                    Failed = result.Failure,
                    AffectedRegistrations = affected
                };

            var logs = new List<PushNotificationLog>();
            foreach (var item in result.Results)
            {
                affected.Add(new RegistrationData
                {
                    RegistrationId = item.RegistrationId,
                    PnsHandle = item.PnsHandle,
                    ApplicationPlatform = item.ApplicationPlatform
                });

                logs.Add(new PushNotificationLog
                {
                    RegistrationId = item.RegistrationId,
                    Handle = item.PnsHandle,
                    Platform = item.ApplicationPlatform,
                    Payload = payload,
                    CreatedAt = createdAt,
                    CreatedBy = Guid.Empty
                });                
            }

            if (!logs.Any())
                return new SendNotificationCommandResult
                {
                    Succeeded = result.Success,
                    Failed = result.Failure,
                    AffectedRegistrations = affected
                };

            await DatabaseContext.PushNotificationLogs.AddRangeAsync(logs, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
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