using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Articles;

[ExcludeFromCodeCoverage]
public class ArticleCategory : Entity<Guid>, IAuditable
{
    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public ICollection<Article> Articles { get; set; } = new HashSet<Article>();

    public ICollection<ArticleCategoryName> ArticleCategoryNames { get; set; } = new HashSet<ArticleCategoryName>();
}