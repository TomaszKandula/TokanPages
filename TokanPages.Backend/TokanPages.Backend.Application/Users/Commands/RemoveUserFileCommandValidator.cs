using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserFileCommandValidator : AbstractValidator<RemoveUserFileCommand>
{
    public RemoveUserFileCommandValidator()
    {
        RuleFor(command => command.Type)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .IsInEnum()
            .WithErrorCode(nameof(ValidationCodes.INVALID_ENUM_VALUE))
            .WithMessage(ValidationCodes.INVALID_ENUM_VALUE);

        RuleFor(command => command.UniqueBlobName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(225)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
    }
}