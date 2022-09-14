using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Articles : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    public bool IsPublished { get; set; }

    public int ReadCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UserId { get; set; }

    public Users UserNavigation { get; set; }

    public ICollection<ArticleLikes> ArticleLikesNavigation { get; set; } = new HashSet<ArticleLikes>();

    public ICollection<ArticleCounts> ArticleCountsNavigation { get; set; } = new HashSet<ArticleCounts>();
}