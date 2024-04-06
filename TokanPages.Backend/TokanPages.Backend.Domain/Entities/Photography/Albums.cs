using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

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

    public Users UserNavigation { get; set; }

    public UserPhotos UserPhotoNavigation { get; set; }
}