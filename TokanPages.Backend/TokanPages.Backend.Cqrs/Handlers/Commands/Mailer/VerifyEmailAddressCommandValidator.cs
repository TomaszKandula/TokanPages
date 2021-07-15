namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using FluentValidation;
    using Shared.Resources;

    public class VerifyEmailAddressCommandValidator : AbstractValidator<VerifyEmailAddressCommand>
    {
        public VerifyEmailAddressCommandValidator() 
        {
            RuleFor(AField => AField.Email)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
        }
    }
}