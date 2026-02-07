using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "FeedImages")]
public class FeedImage : Entity<Guid>, ISoftDelete
{
    public Guid FeedId { get; set; }

    public string ImageBlobName { get; set; }

    public bool IsDeleted { get; set; }
}