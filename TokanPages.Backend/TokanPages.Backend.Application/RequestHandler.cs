using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly OperationsDbContext OperationsDbContext;

    protected readonly ILoggerService LoggerService;

    protected RequestHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService)
    {
        OperationsDbContext = operationsDbContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}