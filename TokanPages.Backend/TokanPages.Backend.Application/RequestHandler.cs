using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly OperationDbContext OperationDbContext;

    protected readonly ILoggerService LoggerService;

    protected RequestHandler(OperationDbContext operationDbContext, ILoggerService loggerService)
    {
        OperationDbContext = operationDbContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}