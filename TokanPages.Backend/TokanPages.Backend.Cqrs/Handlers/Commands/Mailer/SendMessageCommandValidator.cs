namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using FluentValidation;
    using Shared.Resources;

    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator() 
        {
            RuleFor(AField => AField.FirstName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.FIRST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.FIRST_NAME_TOO_LONG);

            RuleFor(AField => AField.LastName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.LAST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.LAST_NAME_TOO_LONG);

            RuleFor(AField => AField.UserEmail)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

            RuleFor(AField => AField.EmailFrom)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

            RuleFor(AField => AField.EmailTos)
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