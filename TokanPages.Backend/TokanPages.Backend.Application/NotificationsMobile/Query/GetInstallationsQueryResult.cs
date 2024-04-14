using TokanPages.Services.PushNotificationService.Models;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetInstallationsQueryResult
{
    public List<DeviceInstallationOutput> Installations { get; set; } = new();
}