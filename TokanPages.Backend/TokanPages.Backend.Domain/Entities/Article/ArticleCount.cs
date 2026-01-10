using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Article;

[ExcludeFromCodeCoverage]
public class ArticleCount : Entity<Guid>, IAuditable
{
    [Required]
    public Guid ArticleId { get; set; }
    public Guid? UserId { get; set; }
    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }
    public int ReadCount { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Article Article { get; set; }
    public User.User User { get; set; }
}