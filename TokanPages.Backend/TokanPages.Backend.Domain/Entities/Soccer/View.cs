using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class View : Entity<Guid>
{
    [Required]
    public Guid PlayerId { get; set; }

    [Required]
    public int Count { get; set; }

    /* Navigation properties */
    public Player Player { get; set; }
}