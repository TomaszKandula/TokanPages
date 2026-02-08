using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly OperationDbContext OperationDbContext;//TODO: to be removed

    protected readonly ILoggerService LoggerService;

    protected RequestHandler(OperationDbContext operationDbContext, ILoggerService loggerService)
    {
        OperationDbContext = operationDbContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}