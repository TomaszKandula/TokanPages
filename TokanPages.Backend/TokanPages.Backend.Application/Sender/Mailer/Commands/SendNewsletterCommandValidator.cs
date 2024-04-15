using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Mailer.Commands;

public class SendNewsletterCommandValidator : AbstractValidator<SendNewsletterCommand>
{
    public SendNewsletterCommandValidator() 
    {
        RuleFor(command => command.SubscriberInfo)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Subject)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.SUBJECT_TOO_LONG))
            .WithMessage(ValidationCodes.SUBJECT_TOO_LONG);

        RuleFor(command => command.Message)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.MESSAGE_TOO_LONG))
            .WithMessage(ValidationCodes.MESSAGE_TOO_LONG);
    }
}