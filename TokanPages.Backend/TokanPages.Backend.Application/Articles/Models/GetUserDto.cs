using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Articles.Models;

/// <summary>
/// Use to get current user.
/// </summary>
[ExcludeFromCodeCoverage]
public class GetUserDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Alias name.
    /// </summary>
    public string? AliasName { get; set; }

    /// <summary>
    /// Avatar name.
    /// </summary>
    public string? AvatarName { get; set; }

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
    public string? Email { get; set; }

    /// <summary>
    /// User short description ('biography').
    /// </summary>
    public string? ShortBio { get; set; }

    /// <summary>
    /// Date and time of registration.
    /// </summary>
    public DateTime Registered { get; set; }
}