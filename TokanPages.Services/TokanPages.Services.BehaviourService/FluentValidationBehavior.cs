namespace TokanPages.Services.BehaviourService;

using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using MediatR;

[ExcludeFromCodeCoverage]
public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public FluentValidationBehavior(IValidator<TRequest>? validator = null) => _validator = validator;

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validator == null) return next();

        var validationContext = new ValidationContext<TRequest>(request);
        var validationResults = _validator.Validate(validationContext);

        if (!validationResults.IsValid)
            throw new TokanPages.Backend.Core.Exceptions.ValidationException(validationResults);

        return next();
    }
}