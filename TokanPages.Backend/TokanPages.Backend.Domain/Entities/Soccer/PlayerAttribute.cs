using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "PlayerAttributes")]
public class PlayerAttribute : Entity<Guid>
{
    public Guid PlayerId { get; set; }

    public Guid AttributeId { get; set; }

    public int Value { get; set; }
}