namespace TokanPages.Backend.Identity.Services.JwtUtilityService
{
    using System;
    using System.Text;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;
    using Shared.Models;
    
    public class JwtUtilityService : IJwtUtilityService
    {
        /// <summary>
        /// Returns a new security token with given claims and expiration date and time.
        /// </summary>
        /// <param name="AExpires">Value of the 'expiration' claim. This value should be in UTC.</param>
        /// <param name="AClaimsIdentity">Claims that will be used when creating a security token.</param>
        /// <param name="AWebSecret">String used to generate token key.</param>
        /// <param name="AIssuer">Issuer of a security token.</param>
        /// <param name="ATargetAudience">Target audience.</param>
        /// <returns>New JSON Web Token.</returns>
        public string GenerateJwt(DateTime AExpires, ClaimsIdentity AClaimsIdentity, string AWebSecret, string AIssuer, string ATargetAudience)
        {
            var LTokenHandler = new JwtSecurityTokenHandler();
            var LKey = Encoding.ASCII.GetBytes(AWebSecret);
            var LTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AIssuer,
                IssuedAt = DateTime.UtcNow,
                Audience = ATargetAudience,
                Subject = AClaimsIdentity,
                Expires = AExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(LKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var LToken = LTokenHandler.CreateToken(LTokenDescriptor);
            return LTokenHandler.WriteToken(LToken);
        }

        /// <summary>
        /// Return new RefreshToken for re-authentication. 
        /// </summary>
        /// <param name="AIpAddress">IP address of the machine that requests new refresh token.</param>
        /// <param name="AExpiresIn">Number of minutes to expire.</param>
        /// <returns>New randomized secure token.</returns>
        public RefreshToken GenerateRefreshToken(string AIpAddress, int AExpiresIn)
        {
            using var LRngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var LRandomBytes = new byte[128];
            
            LRngCryptoServiceProvider.GetBytes(LRandomBytes);
            var LRefreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(LRandomBytes),
                Expires = DateTime.UtcNow.AddMinutes(AExpiresIn),
                Created = DateTime.UtcNow,
                CreatedByIp = AIpAddress
            };

            return LRefreshToken;
        }
    }
}