using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace TokanPages.Backend.Cqrs
{
    [ExcludeFromCodeCoverage]
    public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        protected TemplateHandler() { }

        public abstract Task<TResult> Handle(TRequest ARequest, CancellationToken ACancellationToken);
    }
}
