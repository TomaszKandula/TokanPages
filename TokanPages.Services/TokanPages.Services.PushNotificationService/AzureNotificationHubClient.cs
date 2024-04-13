using System.Text.RegularExpressions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.PushNotificationService.Abstractions;
using TokanPages.Services.PushNotificationService.Exceptions;
using TokanPages.Services.PushNotificationService.Models;
using Microsoft.Azure.NotificationHubs;

namespace TokanPages.Services.PushNotificationService;

public class AzureNotificationHubClient : IAzureNotificationHubClient
{
    private readonly NotificationHubClient _notificationHubClient;

    public AzureNotificationHubClient(string hubName, string connectionString) 
        => _notificationHubClient = NotificationHubClient.CreateClientFromConnectionString(connectionString, hubName);

    public async Task CreateOrUpdateInstallation(DeviceInstallationInput input, CancellationToken cancellationToken = default)
    {
        var installation = new Installation
        {
            InstallationId = input.InstallationId,
            PushChannel = input.Handle,
            Tags = input.Tags,
            Platform = input.Platform switch
            {
                "aps" => NotificationPlatform.Apns,
                "gcm" => NotificationPlatform.Fcm,
                "fcm" => NotificationPlatform.Fcm,
                _ => throw new BusinessException(nameof(ErrorCodes.UNSUPPORTED_PLATFORM), ErrorCodes.UNSUPPORTED_PLATFORM)
            }
        };

        await _notificationHubClient.CreateOrUpdateInstallationAsync(installation, cancellationToken);
    }

    public async Task<List<DeviceInstallationOutput>> GetAllInstallations(CancellationToken cancellationToken = default)
    {
        const string regex = @"(?<=\{).+?(?=\})";

        var registrations = await _notificationHubClient.GetAllRegistrationsAsync(0, cancellationToken);
        var installations = new List<DeviceInstallationOutput>();

        foreach (var item in registrations)
        {
            var apsTags = item.Tags.ToArray();
            var platform = GetPlatformType(item);
            installations.Add(new DeviceInstallationOutput
            {
                RegistrationId = item.RegistrationId,
                InstallationId = Regex.Match(apsTags[0], regex, RegexOptions.IgnoreCase).Value,
                PnsHandle = item.PnsHandle,
                ExpirationTime = item.ExpirationTime.ToString(),
                Platform = platform,
                Tags = apsTags.Skip(1).ToArray()
            });
        }

        return installations;
    }

    public async Task<DeviceInstallationOutput> GetInstallationById(string installationId, CancellationToken cancellationToken = default)
    {
        var registration = await GetAllInstallations(cancellationToken);
        if (registration is null)
            throw new InstallationException("Azure Notification Hub is empty");

        var installation = registration.SingleOrDefault(output => output.InstallationId == installationId);
        if (installation is null)
            throw new InstallationException("Cannot find installation in Azure Notification Hub");

        return installation;
    }

    public async Task DeleteInstallationById(string installationId, CancellationToken cancellationToken = default)
    {
        await _notificationHubClient.DeleteInstallationAsync(installationId, cancellationToken);
    }

    public async Task<NotificationOutcome> SendPushNotification(PlatformType targetPlatform, string payload, IEnumerable<string> tags, CancellationToken cancellationToken = default)
    {
        var enumerable = tags as string[] ?? tags.ToArray();
        if (!enumerable.Any())
            throw new BusinessException(nameof(ErrorCodes.MISSING_TAGS), ErrorCodes.MISSING_TAGS);

        return targetPlatform switch
        {
            PlatformType.Aps => await _notificationHubClient.SendAppleNativeNotificationAsync(payload, enumerable, cancellationToken),
            PlatformType.Gcm => await _notificationHubClient.SendFcmNativeNotificationAsync(payload, enumerable, cancellationToken),
            PlatformType.Fcm => await _notificationHubClient.SendFcmNativeNotificationAsync(payload, enumerable, cancellationToken),
            _ => throw new BusinessException(nameof(ErrorCodes.UNSUPPORTED_PLATFORM), ErrorCodes.UNSUPPORTED_PLATFORM)
        };
    }

    public async Task<NotificationOutcome> SendPushNotification(PlatformType targetPlatform, string payload, CancellationToken cancellationToken = default)
    {
        return targetPlatform switch
        {
            PlatformType.Aps => await _notificationHubClient.SendAppleNativeNotificationAsync(payload, cancellationToken),
            PlatformType.Gcm => await _notificationHubClient.SendFcmNativeNotificationAsync(payload, cancellationToken),
            PlatformType.Fcm => await _notificationHubClient.SendFcmNativeNotificationAsync(payload, cancellationToken),
            _ => throw new BusinessException(nameof(ErrorCodes.UNSUPPORTED_PLATFORM), ErrorCodes.UNSUPPORTED_PLATFORM)
        };
    }

    private static string GetPlatformType(RegistrationDescription description)
    { 
        return description switch
        {
            AppleRegistrationDescription => "aps",
            FcmRegistrationDescription => "fcm",
            _ => string.Empty
        };
    }
}