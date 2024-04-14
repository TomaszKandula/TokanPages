using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
public class Albums : Entity<Guid>, IAuditable
{
    public Guid? UserId { get; set; }
    public Guid? UserPhotoId { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    /* Navigation properties */
    public Users Users { get; set; }
    public UserPhotos UserPhotos { get; set; }
}