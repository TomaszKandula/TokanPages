using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Views")]
public class View : Entity<Guid>
{
    public required Guid PlayerId { get; set; }

    public required int Count { get; set; }
}