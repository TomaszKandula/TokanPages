using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationTagDto
{
    public required Guid PushNotificationId { get; init; }

    public required string Tag { get; init; }
}