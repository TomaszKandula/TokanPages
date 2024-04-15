using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Notification;
using TokanPages.Persistence.Database;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Exceptions;
using TokanPages.Services.PushNotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class AddInstallationCommandHandler : RequestHandler<AddInstallationCommand, AddInstallationCommandResult>
{
    private const int MaxRetryCount = 3;

    private const int DelayMilliseconds = 1000;
    
    private readonly IDateTimeService _dateTimeService;

    private readonly IAzureNotificationHubFactory _azureNotificationHubFactory;

    public AddInstallationCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureNotificationHubFactory azureNotificationHubFactory, IDateTimeService dateTimeService) 
        : base(databaseContext, loggerService)
    {
        _azureNotificationHubFactory = azureNotificationHubFactory;
        _dateTimeService = dateTimeService;
    }

    public override async Task<AddInstallationCommandResult> Handle(AddInstallationCommand request, CancellationToken cancellationToken)
    {
        var hub = _azureNotificationHubFactory.Create();
        var tags = request.Tags.ToArray();

        var notification = await GetInstallationByHandle(request.PnsHandle, cancellationToken);
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

            var pushNotification = new PushNotification
            {
                Id = installationId,
                Handle = installation.Handle,
                Platform = installation.Platform,
                Description = "First PNS handle installation",
                CreatedAt = _dateTimeService.Now,
                CreatedBy = Guid.Empty,
                IsVerified = status.isVerified,
                RegistrationId = status.registrationId
            };

            await RegisterInstallation(installationId, pushNotification, tags, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
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

            await ReplaceExistingTags(notification.Id, tags, cancellationToken);
            LoggerService.LogInformation($"The existing tags for installation ID ({notification.Id}) have been replaced");

            notification.ModifiedAt = _dateTimeService.Now;
            notification.ModifiedBy = Guid.Empty;
            notification.Description = "PNS handle installation updated";
            notification.IsVerified = status.isVerified;
            notification.RegistrationId = status.registrationId;

            await DatabaseContext.SaveChangesAsync(cancellationToken);
            LoggerService.LogInformation("The existing PNS handle installation has been saved within the system database");
        }

        return new AddInstallationCommandResult
        {
            InstallationId = installationId,
            RegistrationId = status.registrationId,
            IsVerified = status.isVerified
        };
    }

    private async Task<PushNotification?> GetInstallationByHandle(string pnsHandle, CancellationToken cancellationToken)
    {
        return await DatabaseContext.PushNotifications
            .SingleOrDefaultAsync(notifications => notifications.Handle == pnsHandle, cancellationToken);
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
                var wording = retryCount == 1 ? "try" : "tries";
                LoggerService.LogWarning($"Could not get installation ID after {retryCount} {wording}, latest exception: {exception.Message}");
            }
        }

        return (false, string.Empty);
    }

    private async Task RegisterInstallation(Guid installationId, PushNotification pushNotification, IEnumerable<string> tags, CancellationToken cancellationToken)
    {
        var notificationTags = new List<PushNotificationTag>();
        foreach (var tag in tags)
        {
            notificationTags.Add(new PushNotificationTag
            {
                Id = Guid.NewGuid(),
                PushNotificationId = installationId,
                Tag = tag,
                CreatedAt = pushNotification.CreatedAt,
                CreatedBy = pushNotification.CreatedBy
            });
        }

        await DatabaseContext.PushNotifications.AddAsync(pushNotification, cancellationToken);
        await DatabaseContext.PushNotificationTags.AddRangeAsync(notificationTags, cancellationToken);
    }

    private async Task ReplaceExistingTags(Guid installationId, IEnumerable<string> newTags, CancellationToken cancellationToken)
    {
        var existingTags = await DatabaseContext.PushNotificationTags
            .Where(tags => tags.PushNotificationId == installationId)
            .ToListAsync(cancellationToken);

        var notificationTags = new List<PushNotificationTag>();
        var createdAt = _dateTimeService.Now;
        var createdBy = Guid.Empty;

        foreach (var tag in newTags)
        {
            notificationTags.Add(new PushNotificationTag
            {
                Id = Guid.NewGuid(),
                PushNotificationId = installationId,
                Tag = tag,
                CreatedAt = createdAt,
                CreatedBy = createdBy
            });
        }

        DatabaseContext.PushNotificationTags.RemoveRange(existingTags);
        await DatabaseContext.PushNotificationTags.AddRangeAsync(notificationTags, cancellationToken);
    }
}