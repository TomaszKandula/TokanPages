using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to update existing password.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserPasswordDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Reset ID.
    /// </summary>
    public Guid? ResetId { get; set; }

    /// <summary>
    /// New password.
    /// </summary>
    public string? NewPassword { get; set; }
}