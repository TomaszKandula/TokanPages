using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Domain.Entities.Article;

[ExcludeFromCodeCoverage]
public class ArticleLikes : Entity<Guid>, IAuditable
{
    [Required]
    public Guid ArticleId { get; set; }
    public Guid? UserId { get; set; }
    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }
    public int LikeCount { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Articles Articles { get; set; }
    public Users Users { get; set; }
}