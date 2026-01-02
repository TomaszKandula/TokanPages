using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Language : Entity<Guid>
{
    [Required]
    [MaxLength(2)]
    public string LangId { get; set; }

    [Required]
    [MaxLength(5)]
    public string HrefLang { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public bool IsDefault { get; set; }
}