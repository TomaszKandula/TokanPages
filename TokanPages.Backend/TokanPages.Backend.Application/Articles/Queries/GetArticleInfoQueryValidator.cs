using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQueryValidator : AbstractValidator<GetArticleInfoQuery>
{
    public GetArticleInfoQueryValidator()
    {
        When(query => query.Id != null, () =>
        {
            RuleFor(query => query.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });
    }
}