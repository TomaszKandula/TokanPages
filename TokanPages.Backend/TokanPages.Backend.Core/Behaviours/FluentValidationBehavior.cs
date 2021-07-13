namespace TokanPages.Backend.Core.Behaviours
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> FValidator;

        public FluentValidationBehavior(IValidator<TRequest> AValidator = null) 
            => FValidator = AValidator;

        public Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
        {
            if (FValidator == null) return ANext();

            var LValidationContext = new ValidationContext<TRequest>(ARequest);
            var LValidationResults = FValidator.Validate(LValidationContext);

            if (!LValidationResults.IsValid)
                throw new Exceptions.ValidationException(LValidationResults);

            return ANext();
        }
    }
}