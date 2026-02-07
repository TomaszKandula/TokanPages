using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notifications;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "PushNotificationLogs")]
public class PushNotificationLog : Entity<Guid>, IAuditable
{
    public string RegistrationId { get; set; }

    public string Handle { get; set; }

    public string Platform { get; set; }

    public string Payload { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}