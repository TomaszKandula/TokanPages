using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notification;

[ExcludeFromCodeCoverage]
public class PushNotification : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string Handle { get; set; }
    [Required]
    [MaxLength(6)]
    public string Platform { get; set; }
    [MaxLength(255)]
    public string Description { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsVerified { get; set; }
    [Required]
    [MaxLength(255)]
    public string RegistrationId { get; set; }

    /* Navigation properties */
    public ICollection<PushNotificationTag> PushNotificationTags { get; set; } = new HashSet<PushNotificationTag>();
}