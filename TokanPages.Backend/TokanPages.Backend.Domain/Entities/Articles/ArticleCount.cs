using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "ArticleCounts")]
public class ArticleCount : Entity<Guid>, IAuditable
{
    public Guid ArticleId { get; set; }

    public Guid? UserId { get; set; }

    public string IpAddress { get; set; }

    public int ReadCount { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}