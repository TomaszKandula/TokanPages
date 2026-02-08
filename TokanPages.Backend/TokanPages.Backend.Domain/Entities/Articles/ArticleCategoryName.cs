using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "ArticleCategoryNames")]
public class ArticleCategoryName : Entity<Guid>
{
    public required Guid ArticleCategoryId { get; set; }

    public required string Name { get; set; }

    public required Guid LanguageId { get; set; }
}