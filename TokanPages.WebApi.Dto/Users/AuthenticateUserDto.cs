namespace TokanPages.WebApi.Dto.Users;

using System.Diagnostics.CodeAnalysis;

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