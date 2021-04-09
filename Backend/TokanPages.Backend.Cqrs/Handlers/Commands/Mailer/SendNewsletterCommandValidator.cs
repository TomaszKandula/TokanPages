using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendNewsletterCommandValidator : AbstractValidator<SendNewsletterCommand>
    {
        public SendNewsletterCommandValidator() 
        {
            RuleFor(AField => AField.SubscriberInfo)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(AField => AField.Subject)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.SUBJECT_TOO_LONG))
                .WithMessage(ValidationCodes.SUBJECT_TOO_LONG);

            RuleFor(AField => AField.Message)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.MESSAGE_TOO_LONG))
                .WithMessage(ValidationCodes.MESSAGE_TOO_LONG);
        }
    }
}
