namespace TokanPages.Backend.Shared.Dto.Users;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class RevokeUserRefreshTokenDto
{
    public string RefreshToken { get; set; }
}