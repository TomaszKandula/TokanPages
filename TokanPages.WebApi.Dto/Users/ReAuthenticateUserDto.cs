namespace TokanPages.Backend.Dto.Users;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Re-authenticate logged user
/// </summary>
[ExcludeFromCodeCoverage]
public class ReAuthenticateUserDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string? RefreshToken { get; set; }
}