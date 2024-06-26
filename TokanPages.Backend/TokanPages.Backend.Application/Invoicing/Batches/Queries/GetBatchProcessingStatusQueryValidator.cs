using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetBatchProcessingStatusQueryValidator : AbstractValidator<GetBatchProcessingStatusQuery>
{
    public GetBatchProcessingStatusQueryValidator()
    {
        RuleFor(query => query.ProcessBatchKey)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}