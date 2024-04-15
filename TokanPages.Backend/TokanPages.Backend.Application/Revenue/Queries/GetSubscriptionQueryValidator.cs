using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Subscriptions.Queries;

public class GetSubscriptionQueryValidator : AbstractValidator<GetSubscriptionQuery>
{
    public GetSubscriptionQueryValidator()
    {
        When(query => query.UserId != null, () =>
        {
            RuleFor(query => query.UserId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
                .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
        });
    }
}