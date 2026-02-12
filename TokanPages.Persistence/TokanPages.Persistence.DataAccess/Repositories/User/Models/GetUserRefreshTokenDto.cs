using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserRefreshTokenDto
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required string Token { get; init; } = string.Empty;

    public required DateTime Expires { get; init; }

    public required DateTime Created { get; init; }

    public string? CreatedByIp { get; init; }

    public DateTime? Revoked { get; init; }

    public string? RevokedByIp { get; init; }

    public string? ReplacedByToken { get; init; }

    public string? ReasonRevoked { get; init; }
}