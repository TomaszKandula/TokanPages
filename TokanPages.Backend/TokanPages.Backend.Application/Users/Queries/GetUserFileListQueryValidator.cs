using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQueryValidator : AbstractValidator<GetUserFileListQuery>
{
    public GetUserFileListQueryValidator()
    {
        RuleFor(query => query.Type)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .IsInEnum()
            .WithErrorCode(nameof(ValidationCodes.INVALID_ENUM_VALUE))
            .WithMessage(ValidationCodes.INVALID_ENUM_VALUE);
    }
}