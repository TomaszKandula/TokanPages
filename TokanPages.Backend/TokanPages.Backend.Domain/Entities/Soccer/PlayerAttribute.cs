using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "PlayerAttributes")]
public class PlayerAttribute : Entity<Guid>
{
    public required Guid PlayerId { get; set; }

    public required Guid AttributeId { get; set; }

    public required int Value { get; set; }
}