using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class PlayerAttribute : Entity<Guid>
{
    public Guid PlayerId { get; set; }

    public Guid AttributeId { get; set; }

    public int Value { get; set; }
}