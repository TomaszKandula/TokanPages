using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoStatusQueryValidator : AbstractValidator<GetVideoStatusQuery>
{
    public GetVideoStatusQueryValidator()
    {
        RuleFor(query => query.TicketId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
            .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
    }
}