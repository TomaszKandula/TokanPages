using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Player : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid PositionId { get; set; }

    [Required]
    [MaxLength(255)]
    public string NickName { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }
    
    public DateTime Birthday { get; set; }

    /* Navigation properties */
    public User User { get; set; }

    public Position Position { get; set; }

    public ICollection<Lineup> Lineups { get; set; } = new HashSet<Lineup>();

    public ICollection<Team> Teams { get; set; } = new HashSet<Team>();

    public ICollection<View> Views { get; set; } = new HashSet<View>();
}