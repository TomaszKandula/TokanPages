using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Field : Entity<Guid>, ISoftDelete
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }    

    public double GpsLatitude { get; set; }

    public double GpsLongitude { get; set; }

    public Guid ImageCollectionId { get; set; }

    public DateTime Published { get; set; }

    public bool IsDeleted { get; set; }
}