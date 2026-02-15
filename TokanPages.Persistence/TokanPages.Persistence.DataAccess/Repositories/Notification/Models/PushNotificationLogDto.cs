using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationLogDto
{
    public required string RegistrationId { get; init; }

    public required string Handle { get; init; }

    public required string Platform { get; init; }

    public required string Payload { get; init; }
}