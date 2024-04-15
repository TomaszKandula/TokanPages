using TokanPages.Services.PushNotificationService.Models;

namespace TokanPages.Backend.Application.NotificationsMobile.Commands;

public class SendNotificationCommandResult
{
    public long Succeeded { get; set; }

    public long Failed { get; set; }

    public IEnumerable<RegistrationData>? AffectedRegistrations { get; set; }
}