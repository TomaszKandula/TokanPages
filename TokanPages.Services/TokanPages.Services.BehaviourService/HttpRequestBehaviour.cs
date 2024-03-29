using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class HttpRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUserService _userService;

    public HttpRequestBehaviour(IUserService userService) => _userService = userService;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var handlerName = typeof(TRequest).Name;
        await _userService.LogHttpRequest(handlerName);
        return await next();
    }
}