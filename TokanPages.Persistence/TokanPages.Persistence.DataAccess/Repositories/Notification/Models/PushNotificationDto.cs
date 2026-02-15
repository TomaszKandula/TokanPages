using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Notification.Models;

[ExcludeFromCodeCoverage]
public class PushNotificationDto
{
    public required Guid Id { get; init; }

    public required string Handle { get; init; }

    public required string Platform { get; init; }

    public required bool IsVerified { get; init; }

    public required string RegistrationId { get; init; }
}