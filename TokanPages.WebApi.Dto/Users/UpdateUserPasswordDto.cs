namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

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