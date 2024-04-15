using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(command => command.Platform)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.MessageTitle)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_225);

        RuleFor(command => command.MessageBody)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(500)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_500))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_500);

        When(command => command.Tags != null,() =>
        {
            RuleFor(command => command.Tags!)
                .Must(tags => tags.Length <= 60)
                .WithErrorCode(nameof(ValidationCodes.TOO_MANY_TAGS))
                .WithMessage(ValidationCodes.TOO_MANY_TAGS);
        });
    }
}