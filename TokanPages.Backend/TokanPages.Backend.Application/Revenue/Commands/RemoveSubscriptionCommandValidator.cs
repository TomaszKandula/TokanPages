using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Subscriptions.Commands;

public class RemoveSubscriptionCommandValidator : AbstractValidator<RemoveSubscriptionCommand>
{
    public RemoveSubscriptionCommandValidator()
    {
        When(command => command.UserId != null, () =>
        {
            RuleFor(command => command.UserId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });
    }
}