using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Newsletters.Commands;

public class RemoveNewsletterCommandValidator : AbstractValidator<RemoveNewsletterCommand>
{
    public RemoveNewsletterCommandValidator() 
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}