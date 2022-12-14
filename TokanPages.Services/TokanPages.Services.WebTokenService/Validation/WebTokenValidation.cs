using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.WebTokenService.Validation;

public class WebTokenValidation : IWebTokenValidation
{
    private const string Authorization = "Authorization";
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly DatabaseContext _databaseContext;

    public WebTokenValidation(IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _databaseContext = databaseContext;
    }

    /// <summary>
    /// Returns provided token for authorization header.
    /// </summary>
    /// <returns>Token or empty string.</returns>
    public string GetWebTokenFromHeader()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers[Authorization];
        if (authorizationHeader is not null && !authorizationHeader.Value.Any()) 
            return string.Empty;

        var token = _httpContextAccessor.HttpContext?.Request.Headers[Authorization].ToArray();
        if (token == null) 
            return string.Empty;

        var bearer = token[0].Split(' ');
        return bearer.Length > 0 && string.IsNullOrEmpty(bearer[1]) 
            ? string.Empty 
            : bearer[1];
    }

    /// <summary>
    /// Checks provided JWT in the header against token saved in the database. 
    /// </summary>
    /// <exception cref="AuthorizationException"></exception>
    public async Task VerifyUserToken()
    {
        var token = GetWebTokenFromHeader();
        var userToken = await _databaseContext.UserTokens
            .AsNoTracking()
            .Where(userTokens => userTokens.Token == token)
            .FirstOrDefaultAsync();

        if (userToken == null)
            throw InvalidUserTokenException;

        if (userToken.Revoked is not null && userToken.RevokedByIp is not null && userToken.ReasonRevoked is not null)
            throw RevokedUserTokenException;
    }

    private static AuthorizationException InvalidUserTokenException 
        => new (nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

    private static AuthorizationException RevokedUserTokenException 
        => new (nameof(ErrorCodes.REVOKED_USER_TOKEN), ErrorCodes.REVOKED_USER_TOKEN);
}