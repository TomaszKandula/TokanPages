using FluentValidation;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Content.Components.Queries;

public class GetPageContentQueryValidator : AbstractValidator<GetPageContentQuery>
{
    public GetPageContentQueryValidator()
    {
        RuleForEach(query => query.Components)
            .SetValidator(new ContentModelValidator());
    }

    private sealed class ContentModelValidator : AbstractValidator<ContentModel>
    {
        public ContentModelValidator()
        {
            RuleFor(query => query.Type)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
            
            RuleFor(query => query.Name)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}