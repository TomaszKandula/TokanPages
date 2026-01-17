using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Attribute : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public double Coefficient { get; set; }
}