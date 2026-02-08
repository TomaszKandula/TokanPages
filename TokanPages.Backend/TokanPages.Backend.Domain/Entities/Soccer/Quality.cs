using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Qualities")]
public class Quality : Entity<Guid>
{
    public required string Rate { get; set; }

    public required int LowerBound  { get; set; }

    public required int UpperBound { get; set; }

    public required string ColourHex  { get; set; }
}