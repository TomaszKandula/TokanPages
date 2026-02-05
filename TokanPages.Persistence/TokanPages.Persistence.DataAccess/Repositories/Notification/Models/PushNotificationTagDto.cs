using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationTagDto
{
    public Guid PushNotificationId { get; set; }

    public string Tag { get; set; } = string.Empty;
}