namespace TokanPages.Backend.Application;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Persistence.Database;
using Core.Utilities.LoggerService;

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