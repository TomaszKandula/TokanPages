using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class UpdateSubscriptionCommandValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionCommandValidator()
    {
        When(command => command.UserId != null, () =>
        {
            RuleFor(command => command.UserId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });

        When(command => command.AutoRenewal != null, () =>
        {
            RuleFor(command => command.AutoRenewal)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.Term != null, () =>
        {
            RuleFor(command => command.Term)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.TotalAmount != null, () =>
        {
            RuleFor(command => command.TotalAmount)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .NotEqual(0)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.CurrencyIso != null, () =>
        {
            RuleFor(command => command.CurrencyIso)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(3)
                .WithErrorCode(nameof(ValidationCodes.ISO_VALUE_TOO_LONG))
                .WithMessage(ValidationCodes.ISO_VALUE_TOO_LONG);
        });
    }
}