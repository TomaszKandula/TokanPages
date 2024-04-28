using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to verify user email.
/// </summary>
[ExcludeFromCodeCoverage]
public class VerifyUserEmailDto
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string EmailAddress { get; set; } = "";
}