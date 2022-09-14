using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class PhotoGears : Entity<Guid>, IAuditable
{
    [MaxLength(100)]
    public string BodyVendor { get; set; }

    [MaxLength(100)]
    public string BodyModel { get; set; }

    [MaxLength(100)]
    public string LensVendor { get; set; }

    [MaxLength(60)]
    public string LensName { get; set; }

    public int FocalLength { get; set; }

    [MaxLength(10)]
    public string ShutterSpeed { get; set; }

    public decimal Aperture { get; set; }

    public int FilmIso { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public ICollection<UserPhotos> UserPhotosNavigation { get; set; } = new HashSet<UserPhotos>();
}