using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.User;

[ExcludeFromCodeCoverage]
public class UserMessageCache : Entity<Guid>
{
    [Required]
    public string ChatKey { get; set; }
    [Required]
    public string Notification { get; set; }
}