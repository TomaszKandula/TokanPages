namespace TokanPages.Backend.Identity.Services.JwtUtilityService
{
    using System;
    using System.Security.Claims;
    using Shared.Models;

    public interface IJwtUtilityService
    {
        string GenerateJwt(DateTime expires, ClaimsIdentity claimsIdentity, string webSecret, string issuer, string targetAudience);

        RefreshToken GenerateRefreshToken(string ipAddress, int expiresIn);
    }
}