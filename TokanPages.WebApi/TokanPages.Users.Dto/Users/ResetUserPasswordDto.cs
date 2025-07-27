using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to reset existing password.
/// </summary>
[ExcludeFromCodeCoverage]
public class ResetUserPasswordDto
{
    /// <summary>
    /// User specific language ID.
    /// </summary>
    public string LanguageId { get; set; } = "";

    /// <summary>
    /// User email address.
    /// </summary>
    public string? EmailAddress { get; set; }     
}