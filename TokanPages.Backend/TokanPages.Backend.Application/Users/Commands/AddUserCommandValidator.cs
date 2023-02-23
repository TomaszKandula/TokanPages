using FluentValidation;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services;

namespace TokanPages.Backend.Application.Users.Commands;

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
            .MinimumLength(8)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_SHORT))
            .WithMessage(ValidationCodes.PASSWORD_TOO_SHORT)
            .MaximumLength(50)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
            .WithMessage(ValidationCodes.PASSWORD_TOO_LONG)
            .Must(PasswordHelpers.HaveSpecialCharacter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_CHAR))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_CHAR)
            .Must(PasswordHelpers.ContainNumber)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_NUMBER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_NUMBER)
            .Must(PasswordHelpers.HaveLargeLetter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER)
            .Must(PasswordHelpers.HaveSmallLetter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER);
    }

}