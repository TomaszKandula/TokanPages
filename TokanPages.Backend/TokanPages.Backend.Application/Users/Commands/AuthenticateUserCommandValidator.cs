using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(command => command.EmailAddress)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
            .WithMessage(ValidationCodes.PASSWORD_TOO_LONG);
    }
}