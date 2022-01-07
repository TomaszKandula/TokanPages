namespace TokanPages.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using UserService;
using Backend.Core.Utilities.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class TokenCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILoggerService _logger;

    private readonly IUserService _userService;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenCheckBehaviour(ILoggerService logger, IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var hasAllowAnonymousAttribute = endpoint != null && endpoint.Metadata.Any(@object => @object is AllowAnonymousAttribute);
        if (hasAllowAnonymousAttribute)
            return await next();

        _logger.LogInformation("JWT verification...");
        await _userService.VerifyUserToken();

        return await next();
    }
}