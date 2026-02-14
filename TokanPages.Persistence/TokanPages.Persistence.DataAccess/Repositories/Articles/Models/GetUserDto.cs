using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

/// <summary>
/// Use to get current user.
/// </summary>
[ExcludeFromCodeCoverage]
public class GetUserDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Alias name.
    /// </summary>
    public string? AliasName { get; init; }

    /// <summary>
    /// Avatar name.
    /// </summary>
    public string? AvatarName { get; init; }

    /// <summary>
    /// First name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// User email address.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User short description ('biography').
    /// </summary>
    public string? ShortBio { get; init; }

    /// <summary>
    /// Date and time of registration.
    /// </summary>
    public DateTime Registered { get; init; }
}