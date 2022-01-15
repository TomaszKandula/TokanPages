namespace TokanPages.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using UserService;
using MediatR;

[ExcludeFromCodeCoverage]
public class HttpRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
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