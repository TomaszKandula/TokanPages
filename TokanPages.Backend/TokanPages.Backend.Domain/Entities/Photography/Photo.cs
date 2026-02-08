using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Photos")]
public class Photo : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required Guid PhotoGearId { get; set; }

    public required Guid PhotoCategoryId { get; set; }

    public required string Keywords { get; set; }

    public required string PhotoUrl { get; set; }

    public required DateTime DateTaken { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }
 
    public DateTime? ModifiedAt { get; set; }
}