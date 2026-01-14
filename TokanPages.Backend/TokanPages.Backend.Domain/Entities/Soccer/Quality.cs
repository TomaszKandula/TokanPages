using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Quality : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Rate { get; set; }

    public int LowerBound  { get; set; }

    public int UpperBound { get; set; }

    [Required]
    [MaxLength(8)]
    public string ColourHex  { get; set; }
}