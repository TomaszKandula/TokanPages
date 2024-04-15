using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class AddNewsletterCommandValidator : AbstractValidator<AddNewsletterCommand>
{
    public AddNewsletterCommandValidator() 
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
    }
}