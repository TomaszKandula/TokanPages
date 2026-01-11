using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using MediatR;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class TableRequestHandler<TEntity, TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly OperationsDbContext OperationsDbContext;

    protected readonly ILoggerService LoggerService;

    protected TableRequestHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService)
    {
        OperationsDbContext = operationsDbContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);

    public abstract IDictionary<string, Expression<Func<TEntity, object>>> GetOrderingExpressions();
}