using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Gateway.Dto.Users;

/// <summary>
/// Re-authenticate logged user.
/// </summary>
[ExcludeFromCodeCoverage]
public class ReAuthenticateUserDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Refresh token value.
    /// </summary>
    public string? RefreshToken { get; set; }
}