using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserRefreshTokenDto
{
    public required Guid UserId { get; set; }

    public required string Token { get; set; }

    public required DateTime Expires { get; set; }

    public required DateTime Created { get; set; }

    public string? CreatedByIp { get; set; }

    public DateTime? Revoked { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    public string? ReasonRevoked { get; set; }
}