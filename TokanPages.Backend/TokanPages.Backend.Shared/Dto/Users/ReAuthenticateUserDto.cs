namespace TokanPages.Backend.Shared.Dto.Users;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ReAuthenticateUserDto
{
    public string RefreshToken { get; set; }
}