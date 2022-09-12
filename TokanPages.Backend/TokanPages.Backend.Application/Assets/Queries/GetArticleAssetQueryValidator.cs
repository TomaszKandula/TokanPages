namespace TokanPages.Backend.Application.Assets.Queries;

using FluentValidation;
using Shared.Resources;

public class GetArticleAssetQueryValidator : AbstractValidator<GetArticleAssetQuery>
{
    public GetArticleAssetQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(query => query.AssetName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.NAME_TOO_LONG))
            .WithMessage(ValidationCodes.NAME_TOO_LONG);
    }
}