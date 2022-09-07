#nullable enable
namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use to get current user
/// </summary>
[ExcludeFromCodeCoverage]
public class GetUserDto
{
    /// <summary>
    /// UserId
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// AliasName
    /// </summary>
    public string? AliasName { get; set; }

    /// <summary>
    /// AvatarName
    /// </summary>
    public string? AvatarName { get; set; }

    /// <summary>
    /// FirstName
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// ShortBio
    /// </summary>
    public string? ShortBio { get; set; }

    /// <summary>
    /// Registered
    /// </summary>
    public DateTime Registered { get; set; }
}