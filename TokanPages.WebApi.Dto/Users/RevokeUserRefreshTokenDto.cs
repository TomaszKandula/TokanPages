using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to revoke existing refresh token
/// </summary>
[ExcludeFromCodeCoverage]
public class RevokeUserRefreshTokenDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? RefreshToken { get; set; }
}