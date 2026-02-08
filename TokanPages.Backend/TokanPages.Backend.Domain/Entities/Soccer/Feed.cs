using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Feeds")]
public class Feed : Entity<Guid>, ISoftDelete
{
    public required Guid PlayerId { get; set; }

    public required string Text { get; set; }

    public required DateTime Published { get; set; }

    public required bool IsVisible { get; set; }

    public required bool IsDeleted { get; set; }
}