using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.Services.BehaviourService;

[ExcludeFromCodeCoverage]
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _loggerService;

    public LoggingBehaviour(ILoggerService loggerService) => _loggerService = loggerService;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TResponse).Name;
        var stopWatch = new Stopwatch();
        try
        {
            _loggerService.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
            stopWatch.Start();
            var response = await next();
            stopWatch.Stop();
            LogRunTime(stopWatch.ElapsedMilliseconds, requestName, true);
            return response;
        }
        catch
        {
            stopWatch.Stop();
            LogRunTime(stopWatch.ElapsedMilliseconds, requestName, false);
            throw;
        }
    }

    private void LogRunTime(long elapsedMilliseconds, string requestName, bool executionSucceeded)
    {
        if (executionSucceeded)
        {
            _loggerService.LogInformation($"Finish: Handle {requestName}, completed execution after {elapsedMilliseconds} ms");
        }
        else
        {
            _loggerService.LogError($"Finish: Handle {requestName}, failed execution after {elapsedMilliseconds} ms");
        }
    }
}