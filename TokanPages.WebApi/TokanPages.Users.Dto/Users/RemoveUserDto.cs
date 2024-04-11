using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to remove existing user.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid? Id { get; set; }
}