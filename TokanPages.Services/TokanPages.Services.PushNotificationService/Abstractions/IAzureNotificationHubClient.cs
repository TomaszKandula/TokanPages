using TokanPages.Backend.Domain.Enums;
using TokanPages.Services.PushNotificationService.Models;
using Microsoft.Azure.NotificationHubs;

namespace TokanPages.Services.PushNotificationService.Abstractions;

public interface IAzureNotificationHubClient
{
    Task CreateOrUpdateInstallation(DeviceInstallationInput input, CancellationToken cancellationToken = default);

    Task<List<DeviceInstallationOutput>> GetAllInstallations(CancellationToken cancellationToken = default);

    Task<DeviceInstallationOutput> GetInstallationById(string installationId, CancellationToken cancellationToken = default);

    Task DeleteInstallationById(string installationId, CancellationToken cancellationToken = default);

    Task<NotificationOutcome> SendPushNotification(PlatformType targetPlatform, string payload, IEnumerable<string> tags, CancellationToken cancellationToken = default);

    Task<NotificationOutcome> SendPushNotification(PlatformType targetPlatform, string payload, CancellationToken cancellationToken = default);
}