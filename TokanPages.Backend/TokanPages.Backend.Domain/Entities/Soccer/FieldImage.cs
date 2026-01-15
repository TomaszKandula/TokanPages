using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class FieldImage : Entity<Guid>, ISoftDelete
{
    public Guid FieldId { get; set; }

    [Required]
    [MaxLength(255)]
    public string ImageBlobName { get; set; }

    public bool IsDeleted { get; set; }

    /* Navigation properties */
    public Field Field { get; set; }
}