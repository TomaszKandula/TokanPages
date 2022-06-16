namespace TokanPages.Backend.Dto.Users;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to revoke existing refresh token
/// </summary>
[ExcludeFromCodeCoverage]
public class RevokeUserRefreshTokenDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string RefreshToken { get; set; } = "";
}