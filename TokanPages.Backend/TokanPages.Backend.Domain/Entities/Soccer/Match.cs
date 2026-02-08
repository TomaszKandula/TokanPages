using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Matches")]
public class Match : Entity<Guid>
{
    public required DateTime EventDate { get; set; }

    public required Guid TeamHostId { get; set; }

    public Guid? TeamGuestId { get; set; }

    public required Guid FieldId { get; set; }

    public required int GoalsHost { get; set; }

    public required int GoalsGuest { get; set; }
    
    public required bool IsInternalGame { get; set; }
}