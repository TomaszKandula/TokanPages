using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Views")]
public class View : Entity<Guid>
{
    public Guid PlayerId { get; set; }

    public int Count { get; set; }
}