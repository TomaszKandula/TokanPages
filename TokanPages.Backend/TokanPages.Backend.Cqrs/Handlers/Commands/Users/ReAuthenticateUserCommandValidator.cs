namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using FluentValidation;
using Shared.Resources;

public class ReAuthenticateUserCommandValidator : AbstractValidator<ReAuthenticateUserCommand>
{
    public ReAuthenticateUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}