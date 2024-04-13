using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.VideoProcessing;

public class VideoProcessingStatusQueryValidator : AbstractValidator<VideoProcessingStatusQuery>
{
    public VideoProcessingStatusQueryValidator()
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