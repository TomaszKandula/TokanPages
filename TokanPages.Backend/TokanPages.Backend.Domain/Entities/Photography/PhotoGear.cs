using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "PhotoGears")]
public class PhotoGear : Entity<Guid>, IAuditable
{
    public required string BodyVendor { get; set; }

    public required string BodyModel { get; set; }

    public required string LensVendor { get; set; }

    public required string LensName { get; set; }

    public required int FocalLength { get; set; }

    public required string ShutterSpeed { get; set; }

    public required decimal Aperture { get; set; }

    public required int FilmIso { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}