using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace TokanPages.Backend.Application.Payments.Queries;

[ExcludeFromCodeCoverage]
public class GetPaymentStatusListQueryValidator : AbstractValidator<GetPaymentStatusListQuery>
{
    public GetPaymentStatusListQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}