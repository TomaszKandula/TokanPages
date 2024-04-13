using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Notification;

[ExcludeFromCodeCoverage]
public class WebNotification: Entity<Guid>
{
    [Required]
    public string Value { get; set; }
}