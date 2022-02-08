namespace TokanPages.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Backend.Core.Utilities.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _loggerService;

    public LoggingBehaviour(ILoggerService loggerService) => _loggerService = loggerService;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _loggerService.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
        var response = await next();
        _loggerService.LogInformation($"Finish: Handle {typeof(TResponse).Name}");
        return response;
    }
}