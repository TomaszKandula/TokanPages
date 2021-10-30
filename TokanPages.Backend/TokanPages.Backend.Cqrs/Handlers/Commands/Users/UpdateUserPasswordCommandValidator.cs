namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            When(command => command.ResetId == null, () =>
            {
                RuleFor(command => command.Id)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);
            });
            
            RuleFor(command => command.NewPassword)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(100)
                .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
                .WithMessage(ValidationCodes.PASSWORD_TOO_LONG);
        }
    }
}