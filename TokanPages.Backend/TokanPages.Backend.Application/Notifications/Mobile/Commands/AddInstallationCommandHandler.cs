using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Exceptions;
using TokanPages.Services.PushNotificationService.Models;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Notification;
using TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class AddInstallationCommandHandler : RequestHandler<AddInstallationCommand, AddInstallationCommandResult>
{
    private const int MaxRetryCount = 3;

    private const int DelayMilliseconds = 1000;

    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    private readonly INotificationRepository _notificationRepository;

    public AddInstallationCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory, INotificationRepository notificationRepository) 
        : base(operationDbContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
        _notificationRepository = notificationRepository;
    }

    public override async Task<AddInstallationCommandResult> Handle(AddInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var tags = request.Tags;

        var notification = await _notificationRepository.GetPushNotification(request.PnsHandle);
        var installationId = notification?.Id ?? Guid.NewGuid();
        (bool isVerified, string registrationId) status;

        if (notification is null)
        {
            var installation = new DeviceInstallationInput
            {
                InstallationId = installationId.ToString(),
                Handle = request.PnsHandle,
                Platform = request.Platform.ToString().ToLower(),
                Tags = tags
            };

            await hub.CreateOrUpdateInstallation(installation, cancellationToken);
            const string successInfo = "The new PNS handle has been registered within the Azure Notification Hub";
            status = await TryVerifyInstallation(hub, installationId, successInfo, cancellationToken);

            var pushNotification = new PushNotificationDto
            {
                Id = installationId,
                Handle = installation.Handle,
                Platform = installation.Platform,
                IsVerified = status.isVerified,
                RegistrationId = status.registrationId
            };

            var notificationTags = new List<PushNotificationTagDto>();
            foreach (var tag in tags)
            {
                notificationTags.Add(new PushNotificationTagDto
                {
                    PushNotificationId = installationId,
                    Tag = tag,
                });
            }

            await _notificationRepository.CreatePushNotification(pushNotification);
            await _notificationRepository.CreatePushNotificationTags(notificationTags);
            LoggerService.LogInformation("The new PNS handle installation has been saved within the system database");
        }
        else
        {
            var installation = new DeviceInstallationInput
            {
                InstallationId = notification.Id.ToString(),
                Handle = request.PnsHandle,
                Platform = request.Platform.ToString().ToLower(),
                Tags = tags
            };

            await hub.CreateOrUpdateInstallation(installation, cancellationToken);
            const string successInfo = "The existing PNS handle has been updated within the Azure Notification Hub";
            status = await TryVerifyInstallation(hub, installationId, successInfo, cancellationToken);

            await ReplaceExistingTags(notification.Id, tags);
            LoggerService.LogInformation($"The existing tags for installation ID ({notification.Id}) have been replaced");

            var notificationDto = new PushNotificationDto
            {
                ModifiedBy = Guid.Empty,
                IsVerified = status.isVerified,
                RegistrationId = status.registrationId
            };

            await _notificationRepository.UpdatePushNotification(notificationDto);
            LoggerService.LogInformation("The existing PNS handle installation has been saved within the system database");
        }

        return new AddInstallationCommandResult
        {
            InstallationId = installationId,
            RegistrationId = status.registrationId,
            IsVerified = status.isVerified
        };
    }

    private async Task<(bool, string)> TryVerifyInstallation(IAzureNotificationHubClient hub, Guid installationId, string onSuccessInfo, CancellationToken cancellationToken)
    {
        var retryCount = MaxRetryCount;
        while (retryCount-- > 0)
        {
            try
            {
                var result = await hub.GetInstallationById(installationId.ToString(), cancellationToken);
                LoggerService.LogInformation(onSuccessInfo);
                return (true, result.RegistrationId);
            }
            catch (InstallationException exception)
            {
                await Task.Delay(DelayMilliseconds, cancellationToken);
                LoggerService.LogWarning($"Could not get installation ID after {retryCount} try/-ies, latest exception: {exception.Message}");
            }
        }

        return (false, string.Empty);
    }

    private async Task ReplaceExistingTags(Guid installationId, IEnumerable<string> newTags)
    {
        var notificationTags = new List<PushNotificationTagDto>();
        foreach (var tag in newTags)
        {
            notificationTags.Add(new PushNotificationTagDto
            {
                PushNotificationId = installationId,
                Tag = tag,
            });
        }

        await _notificationRepository.DeletePushNotificationTag(installationId);
        await _notificationRepository.CreatePushNotificationTags(notificationTags);
    }
}