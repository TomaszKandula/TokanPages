namespace TokanPages.Backend.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;
    using Database;
    using Core.Logger;

    [ExcludeFromCodeCoverage]
    public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        protected readonly DatabaseContext DatabaseContext;

        protected readonly ILogger Logger;

        protected TemplateHandler(DatabaseContext databaseContext, ILogger logger)
        {
            DatabaseContext = databaseContext;
            Logger = logger;
        }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}