namespace TokanPages.Backend.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        protected TemplateHandler() { }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}