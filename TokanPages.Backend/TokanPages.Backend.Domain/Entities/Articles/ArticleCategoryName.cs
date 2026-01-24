using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
public class ArticleCategoryName : Entity<Guid>
{
    public Guid ArticleCategoryId { get; set; }
    public string Name { get; set; }
    public Guid LanguageId { get; set; }
}