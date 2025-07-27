using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to verify user email.
/// </summary>
[ExcludeFromCodeCoverage]
public class VerifyUserEmailDto
{
    /// <summary>
    /// user specific language ID.
    /// </summary>
    public string LanguageId { get; set; } = "";

    /// <summary>
    /// User email address.
    /// </summary>
    public string EmailAddress { get; set; } = "";
}