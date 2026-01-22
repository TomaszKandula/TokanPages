using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Matches")]
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