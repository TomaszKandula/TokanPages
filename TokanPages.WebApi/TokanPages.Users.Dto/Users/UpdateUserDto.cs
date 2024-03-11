using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to update existing user.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Activation flag.
    /// </summary>
    public bool IsActivated { get; set; }

    /// <summary>
    /// User alias.
    /// </summary>
    public string? UserAlias { get; set; }

    /// <summary>
    /// First name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// User email address.
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// User 'about section' text.
    /// </summary>
    public string? UserAboutText { get; set; }

    /// <summary>
    /// User image name/path.
    /// </summary>
    public string? UserImageName { get; set; }

    /// <summary>
    /// User video name/path.
    /// </summary>
    public string? UserVideoName { get; set; }
}