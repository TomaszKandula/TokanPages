namespace TokanPages.Backend.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;
    using Database;
    using Core.Utilities.LoggerService;

    [ExcludeFromCodeCoverage]
    public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        protected readonly DatabaseContext DatabaseContext;

        protected readonly ILoggerService LoggerService;

        protected TemplateHandler(DatabaseContext databaseContext, ILoggerService loggerService)
        {
            DatabaseContext = databaseContext;
            LoggerService = loggerService;
        }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}