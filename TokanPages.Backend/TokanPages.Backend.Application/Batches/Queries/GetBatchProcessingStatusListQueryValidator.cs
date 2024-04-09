using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace TokanPages.Backend.Application.Batches.Queries;

[ExcludeFromCodeCoverage]
public class GetBatchProcessingStatusListQueryValidator : AbstractValidator<GetBatchProcessingStatusListQuery>
{
    public GetBatchProcessingStatusListQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}