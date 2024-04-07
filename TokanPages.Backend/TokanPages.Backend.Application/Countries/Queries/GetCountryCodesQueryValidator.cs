using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace TokanPages.Backend.Application.Countries.Queries;

[ExcludeFromCodeCoverage]
public class GetCountryCodesQueryValidator : AbstractValidator<GetCountryCodesQuery>
{
    public GetCountryCodesQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}