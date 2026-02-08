using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Soccer;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "soccer", TableName = "Players")]
public class Player : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required Guid PositionId { get; set; }

    public required string NickName { get; set; }

    public required int Height { get; set; }

    public required int Weight { get; set; }
    
    public required DateTime Birthday { get; set; }
}