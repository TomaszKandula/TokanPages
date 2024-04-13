using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class VerifyUserEmailCommandValidator : AbstractValidator<VerifyUserEmailCommand>
{
    public VerifyUserEmailCommandValidator()
    {
        RuleFor(command => command.EmailAddress)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
    }
}