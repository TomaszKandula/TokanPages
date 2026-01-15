using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class TeamInfo : Entity<Guid>
{
    public Guid TeamId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [MaxLength(255)]
    public string Avatar { get; set; }

    [MaxLength(255)]
    public string ImageBlobName { get; set; }
}