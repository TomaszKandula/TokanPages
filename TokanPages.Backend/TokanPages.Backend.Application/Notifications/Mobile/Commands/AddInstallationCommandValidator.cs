using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class AddInstallationCommandValidator : AbstractValidator<AddInstallationCommand>
{
    public AddInstallationCommandValidator()
    {
        RuleFor(command => command.PnsHandle)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_225);

        RuleFor(command => command.Tags)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Tags)
            .Must(tags => tags.Length <= 60)
            .WithErrorCode(nameof(ValidationCodes.TOO_MANY_TAGS))
            .WithMessage(ValidationCodes.TOO_MANY_TAGS);
    }
}