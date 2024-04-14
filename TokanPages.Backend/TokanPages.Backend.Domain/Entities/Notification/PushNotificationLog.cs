using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notification;

[ExcludeFromCodeCoverage]
public class PushNotificationLog : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string RegistrationId { get; set; }
    [Required]
    [MaxLength(255)]
    public string Handle { get; set; }
    [Required]
    [MaxLength(6)]
    public string Platform { get; set; }
    [Required]
    [MaxLength(2048)]
    public string Payload { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}