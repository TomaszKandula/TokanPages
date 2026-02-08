using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Positions")]
public class Position : Entity<Guid>
{
    public required string Name { get; set; }

    public required string AltName { get; set; }

    public required int TraditionalNumber { get; set; }
}