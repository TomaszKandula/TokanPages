using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
public class Match : Entity<Guid>
{
    public DateTime EventDate { get; set; }

    public Guid TeamHostId { get; set; }

    public Guid? TeamGuestId { get; set; }

    public Guid FieldId { get; set; }

    public int GoalsHost { get; set; }

    public int GoalsGuest { get; set; }
    
    public bool IsInternalGame { get; set; }

    /* Navigation properties */
    public Team Team { get; set; }

    public Field Field { get; set; }

    public ICollection<Lineup> Lineups { get; set; } = new HashSet<Lineup>();
}