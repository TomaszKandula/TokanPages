namespace TokanPages.Backend.Application.Users.Commands;

using FluentValidation;
using Shared.Resources;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator() 
    {
        RuleFor(command => command.EmailAddress)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.FIRST_NAME_TOO_LONG))
            .WithMessage(ValidationCodes.FIRST_NAME_TOO_LONG);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.LAST_NAME_TOO_LONG))
            .WithMessage(ValidationCodes.LAST_NAME_TOO_LONG);

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
            .WithMessage(ValidationCodes.PASSWORD_TOO_LONG);
    }
}