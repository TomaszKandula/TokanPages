using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "PhotoGears")]
public class PhotoGear : Entity<Guid>, IAuditable
{
    public string BodyVendor { get; set; }

    public string BodyModel { get; set; }

    public string LensVendor { get; set; }

    public string LensName { get; set; }
    public int FocalLength { get; set; }

    public string ShutterSpeed { get; set; }

    public decimal Aperture { get; set; }

    public int FilmIso { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}