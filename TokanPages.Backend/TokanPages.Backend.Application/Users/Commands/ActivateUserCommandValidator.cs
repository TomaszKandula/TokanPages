using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
{
    public ActivateUserCommandValidator()
    {
        RuleFor(command => command.ActivationId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}