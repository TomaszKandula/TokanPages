using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TokanPages.WebApi.Configuration;

/// <summary>
/// Token security handler
/// </summary>
[ExcludeFromCodeCoverage]
public class SecurityHandler : ISecurityTokenValidator
{
    private readonly JwtSecurityTokenHandler _tokenHandler;

    /// <summary>
    /// Keep token validation, default value is 'true'
    /// </summary>
    public bool CanValidateToken => true;

    /// <summary>
    /// Keep token maximum size in bytes
    /// </summary>
    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    /// <summary>
    /// Token security handler implementation
    /// </summary>
    public SecurityHandler() => _tokenHandler = new JwtSecurityTokenHandler();

    /// <summary>
    /// Check if it is possible to read given token
    /// </summary>
    /// <param name="securityToken">Token</param>
    /// <returns>True or False</returns>
    public bool CanReadToken(string securityToken) => _tokenHandler.CanReadToken(securityToken);

    /// <summary>
    /// Validate provided token
    /// </summary>
    /// <param name="securityToken">Token</param>
    /// <param name="validationParameters">Parameters</param>
    /// <param name="validatedToken">Output validated token</param>
    /// <returns>Claims principal</returns>
    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
        out SecurityToken validatedToken)
    {
        var principal = _tokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
        return principal;
    }
}