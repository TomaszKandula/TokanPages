namespace TokanPages.Services.WebTokenService;

using System;
using System.Security.Claims;
using Models;

public interface IWebTokenUtility
{
    string GenerateJwt(DateTime expires, ClaimsIdentity claimsIdentity, string webSecret, string issuer, string targetAudience);

    RefreshToken GenerateRefreshToken(string ipAddress, int expiresIn);
}