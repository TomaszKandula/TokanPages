using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
public class PhotoCategories : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(60)]
    public string CategoryName { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public ICollection<UserPhotos> UserPhotos { get; set; } = new HashSet<UserPhotos>();
}