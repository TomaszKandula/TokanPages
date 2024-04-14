using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetNewsletterQueryValidator : AbstractValidator<GetNewsletterQuery>
{
    public GetNewsletterQueryValidator() 
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}