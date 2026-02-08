using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserRefreshTokens")]
public class UserRefreshToken : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required string Token { get; set; }

    public required DateTime Expires { get; set; }

    public required DateTime Created { get; set; }

    public required string CreatedByIp { get; set; }

    public DateTime? Revoked { get; set; }

    public required string RevokedByIp { get; set; }

    public required string ReplacedByToken { get; set; }

    public required string ReasonRevoked { get; set; }
}