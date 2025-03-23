using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Logger.Commands;

public class LogMessageCommandValidator :  AbstractValidator<LogMessageCommand>
{
    public LogMessageCommandValidator()
    {
        RuleFor(command => command.EventType)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_100))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_100);

        RuleFor(command => command.Severity)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_100))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_100);

        RuleFor(command => command.Message)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(2048)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_2048))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_2048);

        RuleFor(command => command.StackTrace)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(4096)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_4096))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_4096);

        RuleFor(command => command.PageUrl)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(2048)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_2048))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_2048);

        RuleFor(command => command.BrowserName)
            .MaximumLength(225)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_225);

        RuleFor(command => command.BrowserVersion)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_100))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_100);

        RuleFor(command => command.UserAgent)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(225)
            .WithErrorCode(nameof(ValidationCodes.LENGTH_TOO_LONG_225))
            .WithMessage(ValidationCodes.LENGTH_TOO_LONG_225);
    }
}