using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetMetricsQueryValidator : AbstractValidator<GetMetricsQuery>
{
    public GetMetricsQueryValidator()
    {
        RuleFor(query => query.Project)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(query => query.Metric)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}