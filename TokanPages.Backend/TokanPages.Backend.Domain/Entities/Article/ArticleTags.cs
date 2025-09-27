using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Article;

[ExcludeFromCodeCoverage]
public class ArticleTags : Entity<Guid>, IAuditable
{
    public Guid ArticleId { get; set; }

    [Required]
    [MaxLength(255)]
    public string TagName { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Articles Articles { get; set; }
}