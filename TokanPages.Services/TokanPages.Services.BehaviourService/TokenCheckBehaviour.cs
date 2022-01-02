namespace TokanPages.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Services.UserServiceProvider;
using MediatR;

[ExcludeFromCodeCoverage]
public class TokenCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILoggerService _logger;

    private readonly IUserServiceProvider _userServiceProvider;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenCheckBehaviour(ILoggerService logger, IHttpContextAccessor httpContextAccessor, IUserServiceProvider userServiceProvider)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _userServiceProvider = userServiceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var hasAllowAnonymousAttribute = endpoint != null && endpoint.Metadata.Any(@object => @object is AllowAnonymousAttribute);
        if (hasAllowAnonymousAttribute)
            return await next();

        _logger.LogInformation("JWT verification...");
        await _userServiceProvider.VerifyUserToken();

        return await next();
    }
}