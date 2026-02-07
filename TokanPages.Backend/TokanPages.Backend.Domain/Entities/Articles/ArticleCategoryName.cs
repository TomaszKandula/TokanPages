using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "ArticleCategoryNames")]
public class ArticleCategoryName : Entity<Guid>
{
    public Guid ArticleCategoryId { get; set; }

    public string Name { get; set; }

    public Guid LanguageId { get; set; }
}