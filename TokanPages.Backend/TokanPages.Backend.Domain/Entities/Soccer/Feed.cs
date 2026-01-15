using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Feed : Entity<Guid>, ISoftDelete
{
    public Guid PlayerId { get; set; }

    public Guid ImageCollectionId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; set; }

    public DateTime Published { get; set; }

    public bool IsVisible { get; set; }

    public bool IsDeleted { get; set; }

    /* Navigation properties */
    public Player Player { get; set; }

    public FeedImage FeedImage { get; set; }

    public ICollection<FeedImage> FeedImages { get; set; } = new HashSet<FeedImage>();
}