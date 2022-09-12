namespace TokanPages.Backend.Application.Content.Queries;

using FluentValidation;
using Shared.Resources;

public class GetContentQueryValidator : AbstractValidator<GetContentQuery>
{
    public GetContentQueryValidator()
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