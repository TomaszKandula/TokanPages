namespace TokanPages.Backend.Application.Handlers.Queries.Assets;

using FluentValidation;
using Shared.Resources;

public class GetSingleAssetQueryValidator : AbstractValidator<GetSingleAssetQuery>
{
    public GetSingleAssetQueryValidator()
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