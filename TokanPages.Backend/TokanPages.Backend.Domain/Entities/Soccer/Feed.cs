using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Feed : Entity<Guid>, ISoftDelete
{
    public Guid PlayerId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; set; }

    public DateTime Published { get; set; }

    public bool IsVisible { get; set; }

    public bool IsDeleted { get; set; }
}