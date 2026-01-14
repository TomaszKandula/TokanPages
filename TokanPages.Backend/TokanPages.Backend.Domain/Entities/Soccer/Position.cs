using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Position : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
}