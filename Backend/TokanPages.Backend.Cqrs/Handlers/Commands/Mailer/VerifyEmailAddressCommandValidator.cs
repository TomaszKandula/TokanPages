using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class VerifyEmailAddressCommandValidator : AbstractValidator<VerifyEmailAddressCommand>
    {
        public VerifyEmailAddressCommandValidator() 
        {
            RuleFor(Field => Field.Email)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
        }
    }
}
