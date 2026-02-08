using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "ArticleLikes")]
public class ArticleLike : Entity<Guid>, IAuditable
{
    public required Guid ArticleId { get; set; }

    public Guid? UserId { get; set; }

    public required string IpAddress { get; set; }

    public required int LikeCount { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}