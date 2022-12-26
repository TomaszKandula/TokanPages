using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.NotificationsWeb.Command;

public class NotifyRequestCommandValidator : AbstractValidator<NotifyRequestCommand>
{
    public NotifyRequestCommandValidator()
    {
        RuleFor(command => command.Handler)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.NAME_TOO_LONG))
            .WithMessage(ValidationCodes.NAME_TOO_LONG);
    }
}