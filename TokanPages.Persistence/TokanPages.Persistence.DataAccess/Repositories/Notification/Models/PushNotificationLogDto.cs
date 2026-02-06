using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationLogDto
{
    public string RegistrationId { get; set; } = string.Empty;

    public string Handle { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;
}