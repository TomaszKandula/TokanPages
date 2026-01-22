using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Qualities")]
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