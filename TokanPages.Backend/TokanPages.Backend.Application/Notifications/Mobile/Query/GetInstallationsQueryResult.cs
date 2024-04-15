using TokanPages.Services.PushNotificationService.Models;

namespace TokanPages.Backend.Application.Notifications.Mobile.Query;

public class GetInstallationsQueryResult
{
    public List<DeviceInstallationOutput> Installations { get; set; } = new();
}