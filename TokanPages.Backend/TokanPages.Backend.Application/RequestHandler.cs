using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Utility.Abstractions;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly ILoggerService LoggerService;

    protected RequestHandler(ILoggerService loggerService) => LoggerService = loggerService;

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}