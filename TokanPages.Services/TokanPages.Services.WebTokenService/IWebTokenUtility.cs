using System.Security.Claims;
using TokanPages.Services.WebTokenService.Models;

namespace TokanPages.Services.WebTokenService;

public interface IWebTokenUtility
{
    string GenerateJwt(DateTime expires, ClaimsIdentity claimsIdentity, string webSecret, string issuer, string targetAudience);

    RefreshToken GenerateRefreshToken(string? ipAddress, int expiresIn, int timezoneOffset = 0);
}