using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class TokenCheckBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    private readonly IWebTokenValidation _webTokenValidation;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenCheckBehaviour(ILoggerService logger, IHttpContextAccessor httpContextAccessor, IWebTokenValidation webTokenValidation)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _webTokenValidation = webTokenValidation;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var endpoint = _httpContextAccessor.HttpContext?.Features.Get<IEndpointFeature>()?.Endpoint;
        var attributes = endpoint?.Metadata.Where(@object => @object is AllowAnonymousAttribute or AuthorizeUserAttribute);

        var hasAllowAnonymousOnly = attributes != null && !attributes.Any(@object => @object is AuthorizeUserAttribute);
        if (hasAllowAnonymousOnly)
            return await next();

        await _webTokenValidation.VerifyUserToken();
        _logger.LogInformation("JWT verification... passed");

        return await next();
    }
}