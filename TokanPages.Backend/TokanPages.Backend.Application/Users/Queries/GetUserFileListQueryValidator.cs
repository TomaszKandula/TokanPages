using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQueryValidator : AbstractValidator<GetUserFileListQuery>
{
    public GetUserFileListQueryValidator()
    {
        RuleFor(query => query.Type)
            .IsInEnum()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}