namespace TokanPages.Backend.Dto.Users;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class RevokeUserRefreshTokenDto
{
    public string RefreshToken { get; set; }
}