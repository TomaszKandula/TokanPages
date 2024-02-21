using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Gateway.Dto.Users;

/// <summary>
/// Use it when you want to sign-in user
/// </summary>
[ExcludeFromCodeCoverage]
public class AuthenticateUserDto
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// User password.
    /// </summary>
    public string? Password { get; set; }
}