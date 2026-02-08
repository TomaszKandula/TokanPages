using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "FeedImages")]
public class FeedImage : Entity<Guid>, ISoftDelete
{
    public required Guid FeedId { get; set; }

    public required string ImageBlobName { get; set; }

    public required bool IsDeleted { get; set; }
}