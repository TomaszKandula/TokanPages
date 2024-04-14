using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentTypeQueryValidator : AbstractValidator<GetPaymentTypeQuery>
{
    public GetPaymentTypeQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}