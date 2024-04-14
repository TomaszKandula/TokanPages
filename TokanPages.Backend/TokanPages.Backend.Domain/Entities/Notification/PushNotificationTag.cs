using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Notification;

[ExcludeFromCodeCoverage]
public class PushNotificationTag : Entity<Guid>, IAuditable
{
    public Guid PushNotificationId { get; set; }
    public string Tag { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public PushNotification PushNotification { get; set; }
}