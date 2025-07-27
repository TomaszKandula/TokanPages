using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to register user account
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserDto
{
    /// <summary>
    /// User specific language ID.
    /// </summary>
    public string LanguageId { get; set; } = "";

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; set; } = "";

    /// <summary>
    /// User email address.
    /// </summary>
    public string EmailAddress { get; set; } = "";

    /// <summary>
    /// User account password.
    /// </summary>
    public string Password { get; set; } = "";
}