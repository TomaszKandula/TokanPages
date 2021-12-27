﻿namespace TokanPages.Backend.Core.Behaviours;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Utilities.LoggerService;
using MediatR;

[ExcludeFromCodeCoverage]
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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