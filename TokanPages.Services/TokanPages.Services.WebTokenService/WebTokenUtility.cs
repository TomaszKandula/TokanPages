using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TokanPages.Services.WebTokenService.Abstractions;
using TokanPages.Services.WebTokenService.Models;

namespace TokanPages.Services.WebTokenService;

public class WebTokenUtility : IWebTokenUtility
{
    private readonly RandomNumberGenerator _numberGenerator;

    public WebTokenUtility() => _numberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Returns a new security token with given claims and expiration date and time.
    /// </summary>
    /// <param name="expires">Value of the 'expiration' claim. This value should be in UTC.</param>
    /// <param name="claimsIdentity">Claims that will be used when creating a security token.</param>
    /// <param name="webSecret">String used to generate token key.</param>
    /// <param name="issuer">Issuer of a security token.</param>
    /// <param name="targetAudience">Target audience.</param>
    /// <returns>New JSON Web Token.</returns>
    public virtual string GenerateJwt(DateTime expires, ClaimsIdentity claimsIdentity, string webSecret, string issuer, string targetAudience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(webSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            IssuedAt = DateTime.UtcNow,
            Audience = targetAudience,
            Subject = claimsIdentity,
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Return new RefreshToken for re-authentication. 
    /// </summary>
    /// <param name="ipAddress">IP address of the machine that requests new refresh token.</param>
    /// <param name="expiresIn">Number of minutes to expire. Cannot be zero.</param>
    /// <param name="timezoneOffset"></param>
    /// <returns>New randomized secure token.</returns>
    public virtual RefreshToken GenerateRefreshToken(string? ipAddress, int expiresIn, int timezoneOffset = 0)
    {
        if (expiresIn == 0)
            throw new ArgumentException($"Argument '{nameof(expiresIn)}' cannot be zero.");

        var randomBytes = new byte[256];
        _numberGenerator.GetBytes(randomBytes);

        var baseDateTime = DateTime.UtcNow.AddMinutes(-timezoneOffset);
        var expires = baseDateTime.AddMinutes(Math.Abs(expiresIn));

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = expires,
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;
    }
}