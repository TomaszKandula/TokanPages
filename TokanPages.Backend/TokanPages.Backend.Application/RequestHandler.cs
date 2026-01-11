using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly DatabaseContext DatabaseContext;

    protected readonly ILoggerService LoggerService;

    protected RequestHandler(DatabaseContext databaseContext, ILoggerService loggerService)
    {
        DatabaseContext = databaseContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}