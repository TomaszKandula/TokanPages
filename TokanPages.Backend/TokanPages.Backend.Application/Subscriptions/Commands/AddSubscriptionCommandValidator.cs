using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class AddSubscriptionCommandValidator : AbstractValidator<AddSubscriptionCommand>
{
    public AddSubscriptionCommandValidator()
    {
        When(command => command.UserId != null, () =>
        {
            RuleFor(command => command.UserId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });

        RuleFor(command => command.SelectedTerm)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        When(command => command.UserCurrency != null, () =>
        {
            RuleFor(command => command.UserCurrency)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(3)
                .WithErrorCode(nameof(ValidationCodes.ISO_VALUE_TOO_LONG))
                .WithMessage(ValidationCodes.ISO_VALUE_TOO_LONG);
        });

        When(command => command.UserLanguage != null, () =>
        {
            RuleFor(command => command.UserLanguage)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(3)
                .WithErrorCode(nameof(ValidationCodes.ISO_VALUE_TOO_LONG))
                .WithMessage(ValidationCodes.ISO_VALUE_TOO_LONG);
        });
    }
}