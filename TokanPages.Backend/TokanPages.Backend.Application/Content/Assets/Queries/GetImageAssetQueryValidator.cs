using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetImageAssetQueryValidator : AbstractValidator<GetImageAssetQuery>
{
    public GetImageAssetQueryValidator()
    {
        RuleFor(query => query.BlobName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.NAME_TOO_LONG))
            .WithMessage(ValidationCodes.NAME_TOO_LONG);
    }
}