using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to reset existing password.
/// </summary>
[ExcludeFromCodeCoverage]
public class ResetUserPasswordDto
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string? EmailAddress { get; set; }     
}