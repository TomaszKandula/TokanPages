namespace TokanPages.Backend.Application.Users.Commands;

using FluentValidation;
using Shared.Resources;

public class ReAuthenticateUserCommandValidator : AbstractValidator<ReAuthenticateUserCommand>
{
    public ReAuthenticateUserCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}