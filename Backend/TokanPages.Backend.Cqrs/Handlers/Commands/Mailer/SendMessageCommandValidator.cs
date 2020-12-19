using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {

        public SendMessageCommandValidator() 
        {

            RuleFor(Field => Field.FirstName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.FIRST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.FIRST_NAME_TOO_LONG);

            RuleFor(Field => Field.LastName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.LAST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.LAST_NAME_TOO_LONG);

            RuleFor(Field => Field.UserEmail)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

            RuleFor(Field => Field.EmailFrom)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

            RuleFor(Field => Field.EmailTos)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(Field => Field.Subject)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.SUBJECT_TOO_LONG))
                .WithMessage(ValidationCodes.SUBJECT_TOO_LONG);

            RuleFor(Field => Field.Message)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.MESSAGE_TOO_LONG))
                .WithMessage(ValidationCodes.MESSAGE_TOO_LONG);

        }

    }

}
