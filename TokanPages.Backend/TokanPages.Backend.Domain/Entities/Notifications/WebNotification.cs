using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Notifications;

[ExcludeFromCodeCoverage]
public class WebNotification: Entity<Guid>
{
    [Required]
    public string Value { get; set; }
}