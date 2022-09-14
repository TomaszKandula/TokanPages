using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to update existing password
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserPasswordDto
{
    /// <summary>
    /// Optional
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public Guid? ResetId { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? NewPassword { get; set; }
}