using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Users;

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