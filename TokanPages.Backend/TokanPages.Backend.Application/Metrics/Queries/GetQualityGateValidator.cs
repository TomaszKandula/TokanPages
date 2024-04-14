using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Metrics.Queries;

public class GetQualityGateValidator : AbstractValidator<GetQualityGateQuery>
{
    public GetQualityGateValidator()
    {
        RuleFor(query => query.Project)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}