using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator() 
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}