namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class RevokeUserRefreshTokenCommandValidator : AbstractValidator<RevokeUserRefreshTokenCommand>
    {
        public RevokeUserRefreshTokenCommandValidator()
        {
            RuleFor(AField => AField.RefreshToken)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}