using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommandValidator : AbstractValidator<RevokeUserRefreshTokenCommand>
{
    public RevokeUserRefreshTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}