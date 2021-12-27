namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using FluentValidation;
using Shared.Resources;

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