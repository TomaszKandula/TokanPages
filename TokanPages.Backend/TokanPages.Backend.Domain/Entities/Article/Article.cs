using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Domain.Entities.Article;

[ExcludeFromCodeCoverage]
public class Article : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
    public bool IsPublished { get; set; }
    public int ReadCount { get; set; }
    public int TotalLikes { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UserId { get; set; }
    public Guid? CategoryId {get; set; }
    [Required]
    [MaxLength(3)]
    public string LanguageIso { get; set; }

    /* Navigation properties */
    public Users Users { get; set; }
    public ArticleCategory ArticleCategory { get; set; }
    public ICollection<ArticleTags> ArticleTags { get; set; } =  new HashSet<ArticleTags>();
    public ICollection<ArticleLike> ArticleLikes { get; set; } = new HashSet<ArticleLike>();
    public ICollection<ArticleCount> ArticleCounts { get; set; } = new HashSet<ArticleCount>();
}