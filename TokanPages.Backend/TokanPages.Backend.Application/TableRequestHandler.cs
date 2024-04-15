using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using MediatR;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class TableRequestHandler<TEntity, TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly DatabaseContext DatabaseContext;

    protected readonly ILoggerService LoggerService;

    protected TableRequestHandler(DatabaseContext databaseContext, ILoggerService loggerService)
    {
        DatabaseContext = databaseContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
        
    public abstract IDictionary<string, Expression<Func<TEntity, object>>> GetOrderingExpressions();
}