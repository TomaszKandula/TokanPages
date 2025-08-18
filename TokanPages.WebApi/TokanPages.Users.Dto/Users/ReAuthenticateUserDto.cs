using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

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
}