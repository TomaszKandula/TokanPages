using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Lineups")]
public class Lineup : Entity<Guid>
{
    public Guid MatchId { get; set; }

    public Guid PlayerHostId { get; set; }

    public Guid PlayerGuestId { get; set; }
}