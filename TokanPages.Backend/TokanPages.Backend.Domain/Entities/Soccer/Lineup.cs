using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Lineup : Entity<Guid>
{
    public Guid MatchId { get; set; }

    public Guid PlayerHostId { get; set; }

    public Guid PlayerGuestId { get; set; }

    /* Navigation properties */
    public Match Match { get; set; }

    public Player Player { get; set; }
}