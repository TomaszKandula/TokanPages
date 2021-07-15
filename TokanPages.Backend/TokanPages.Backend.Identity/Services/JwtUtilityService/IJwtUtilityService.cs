namespace TokanPages.Backend.Identity.Services.JwtUtilityService
{
    using System;
    using System.Security.Claims;
    using Shared.Models;

    public interface IJwtUtilityService
    {
        string GenerateJwt(DateTime AExpires, ClaimsIdentity AClaimsIdentity, string AWebSecret, string AIssuer, string ATargetAudience);

        RefreshToken GenerateRefreshToken(string AIpAddress, int AExpiresIn);
    }
}