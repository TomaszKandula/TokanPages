namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            When(AField => AField.ResetId == null, () =>
            {
                RuleFor(AField => AField.Id)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);
            });
            
            RuleFor(AField => AField.NewPassword)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(100)
                .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
                .WithMessage(ValidationCodes.PASSWORD_TOO_LONG);
        }
    }
}