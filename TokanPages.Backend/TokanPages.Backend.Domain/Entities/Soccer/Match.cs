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
}