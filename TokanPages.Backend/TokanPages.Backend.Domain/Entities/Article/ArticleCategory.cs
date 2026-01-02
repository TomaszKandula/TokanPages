using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Article;

[ExcludeFromCodeCoverage]
public class ArticleCategory : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string CategoryName { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? LanguageId { get; set; }

    /* Navigation properties */
    public Language Language { get; set; }

    public ICollection<Articles> Articles { get; set; } = new HashSet<Articles>();
}