using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace TokanPages.Backend.Application.Currencies.Queries;

[ExcludeFromCodeCoverage]
public class GetCurrencyCodesQueryValidator : AbstractValidator<GetCurrencyCodesQuery>
{
    public GetCurrencyCodesQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}