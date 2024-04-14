using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
public class UserPhotos : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    public Guid PhotoGearId { get; set; }
    public Guid PhotoCategoryId { get; set; }
    [MaxLength(500)]
    public string Keywords { get; set; }
    [Required]
    [MaxLength(255)]
    public string PhotoUrl { get; set; }
    public DateTime DateTaken { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Users Users { get; set; }
    public PhotoGears PhotoGears { get; set; }
    public PhotoCategories PhotoCategories { get; set; }
    public ICollection<Albums> Albums { get; set; } = new HashSet<Albums>();
}