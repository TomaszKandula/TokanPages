using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

[SuppressMessage("Sonar Code Smell", "S3267:Loop should be simplified with LINQ expression", 
    Justification = "LINQ would actually just make things harder to understand")]
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
            .Must(HaveSpecialCharacter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_CHAR))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_CHAR)
            .Must(ContainNumber)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_NUMBER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_NUMBER)
            .Must(HaveLargeLetter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_LARGE_LETTER)
            .Must(HaveSmallLetter)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER))
            .WithMessage(ValidationCodes.PASSWORD_MISSING_SMALL_LETTER);
    }

    private static bool HaveSpecialCharacter(string value)
    {
        var characters = new [] { '!', '@', '#', '$', '%', '^', '&', '*' };
        foreach (var character in value)
        {
            if (characters.Contains(character)) 
                return true;
        }

        return false;
    }

    private static bool ContainNumber(string value)
    {
        foreach (var character in value)
        {
            if (character >= 48 && character <= 57)
                return true;
        }

        return false;
    }

    private static bool HaveLargeLetter(string value)
    {
        foreach (var character in value)
        {
            if (character >= 65 && character <= 90)
                return true;
        }

        return false;
    }

    private static bool HaveSmallLetter(string value)
    {
        foreach (var character in value)
        {
            if (character >= 97 && character <= 122)
                return true;
        }

        return false;
    }
}