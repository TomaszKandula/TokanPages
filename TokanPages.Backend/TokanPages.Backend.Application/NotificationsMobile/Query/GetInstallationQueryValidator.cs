using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetInstallationQueryValidator : AbstractValidator<GetInstallationQuery>
{
    public GetInstallationQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
            .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
    }
}