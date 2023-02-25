using FluentValidation;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        When(command => command.Id != null, () =>
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.ResetId != null, () =>
        {
            RuleFor(command => command.ResetId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.OldPassword != null, () =>
        {
            RuleFor(command => command.OldPassword)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        RuleFor(command => command.NewPassword)
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