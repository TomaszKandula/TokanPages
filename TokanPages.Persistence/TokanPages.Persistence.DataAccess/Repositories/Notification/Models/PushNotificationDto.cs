using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationDto
{
    public Guid Id { get; set; }

    public string Handle { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public DateTime ModifiedAt { get; set; }

    public Guid ModifiedBy { get; set; }

    public bool IsVerified { get; set; }

    public string RegistrationId { get; set; } = string.Empty;
}