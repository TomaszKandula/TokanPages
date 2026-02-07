using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Photography;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "Photos")]
public class Photo : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }

    public Guid PhotoGearId { get; set; }

    public Guid PhotoCategoryId { get; set; }

    public string Keywords { get; set; }

    public string PhotoUrl { get; set; }

    public DateTime DateTaken { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }
 
    public DateTime? ModifiedAt { get; set; }
}