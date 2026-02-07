using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Feeds")]
public class Feed : Entity<Guid>, ISoftDelete
{
    public Guid PlayerId { get; set; }

    public string Text { get; set; }

    public DateTime Published { get; set; }

    public bool IsVisible { get; set; }

    public bool IsDeleted { get; set; }
}