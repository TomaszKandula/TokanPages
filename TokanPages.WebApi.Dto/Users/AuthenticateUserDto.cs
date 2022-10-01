using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to sign-in user
/// </summary>
[ExcludeFromCodeCoverage]
public class AuthenticateUserDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Password { get; set; }
}