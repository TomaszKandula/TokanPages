using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class UpdateNewsletterCommandValidator : AbstractValidator<UpdateNewsletterCommand>
{
    public UpdateNewsletterCommandValidator() 
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

        When(command => command.Count != null, () => 
        {
            RuleFor(command => command.Count)
                .GreaterThan(-1)
                .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                .WithMessage(ValidationCodes.LESS_THAN_ZERO);
        });
    }
}