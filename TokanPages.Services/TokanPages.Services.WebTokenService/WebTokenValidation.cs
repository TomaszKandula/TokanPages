using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.WebTokenService;

[ExcludeFromCodeCoverage]
internal sealed class WebTokenValidation : IWebTokenValidation
{
    private const string Authorization = "Authorization";
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUserRepository _userRepository;

    public WebTokenValidation(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
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
        var userToken = await _userRepository.DoesUserTokenExist(token);
        if (!userToken)
            throw InvalidUserTokenException;
    }

    private static AuthorizationException InvalidUserTokenException 
        => new (nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);
}