using FluentValidation;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class RequestPageDataCommandValidator : AbstractValidator<RequestPageDataCommand>
{
    public RequestPageDataCommandValidator()
    {
        RuleFor(query => query.Components)
            .Must(content => content.Count != 0)
            .WithErrorCode(nameof(ValidationCodes.LIST_EMPTY))
            .WithMessage(ValidationCodes.LIST_EMPTY);

        RuleForEach(query => query.Components)
            .SetValidator(new ContentModelValidator());
    }

    private sealed class ContentModelValidator : AbstractValidator<ContentModel>
    {
        public ContentModelValidator()
        {
            RuleFor(query => query.ContentName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}