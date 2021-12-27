namespace TokanPages.Backend.Core.Utilities.JwtUtilityService;

using System;
using System.Security.Claims;
using Models;

public interface IJwtUtilityService
{
    string GenerateJwt(DateTime expires, ClaimsIdentity claimsIdentity, string webSecret, string issuer, string targetAudience);

    RefreshToken GenerateRefreshToken(string ipAddress, int expiresIn);
}