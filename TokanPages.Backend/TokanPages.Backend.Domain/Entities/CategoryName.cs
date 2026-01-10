using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class CategoryName : Entity<Guid>
{
    public Guid ArticleCategoryId { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    public Guid LanguageId { get; set; }

    /* Navigation properties */
    public ArticleCategory ArticleCategory { get; set; }
    public Language Language { get; set; }
}