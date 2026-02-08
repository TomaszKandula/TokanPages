using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Lineups")]
public class Lineup : Entity<Guid>
{
    public required Guid MatchId { get; set; }

    public required Guid PlayerHostId { get; set; }

    public required Guid PlayerGuestId { get; set; }
}