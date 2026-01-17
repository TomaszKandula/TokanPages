using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.WebTokenService;

public class WebTokenValidation : IWebTokenValidation
{
    private const string Authorization = "Authorization";
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly OperationDbContext _operationDbContext;

    public WebTokenValidation(IHttpContextAccessor httpContextAccessor, OperationDbContext operationDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _operationDbContext = operationDbContext;
    }

    /// <summary>
    /// Returns provided token for authorization header.
    /// </summary>
    /// <returns>Token or empty string.</returns>
    public string GetWebTokenFromHeader()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers[Authorization];
        if (authorizationHeader is not null && authorizationHeader.Value.Count == 0) 
            return string.Empty;

        var token = _httpContextAccessor.HttpContext?.Request.Headers[Authorization].ToArray();
        var bearer = token?[0]?.Split(' ');
        if (bearer is null) 
            return string.Empty;

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
        var userToken = await _operationDbContext.UserTokens
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