using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notifications;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "PushNotifications")]
public class PushNotification : Entity<Guid>, IAuditable
{
    public required string Handle { get; set; }

    public required string Platform { get; set; }

    public required string Description { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public required bool IsVerified { get; set; }

    public required string RegistrationId { get; set; }
}