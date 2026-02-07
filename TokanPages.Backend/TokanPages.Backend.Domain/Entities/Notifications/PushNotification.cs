using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notifications;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "PushNotifications")]
public class PushNotification : Entity<Guid>, IAuditable
{
    public string Handle { get; set; }

    public string Platform { get; set; }

    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsVerified { get; set; }

    public string RegistrationId { get; set; }
}