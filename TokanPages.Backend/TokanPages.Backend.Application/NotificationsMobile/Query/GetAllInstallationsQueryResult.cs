using TokanPages.Services.PushNotificationService.Models;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetAllInstallationsQueryResult
{
    public List<DeviceInstallationOutput> Installations { get; set; } = new();
}