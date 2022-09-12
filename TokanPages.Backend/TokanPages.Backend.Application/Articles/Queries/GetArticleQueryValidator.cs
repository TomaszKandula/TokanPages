namespace TokanPages.Backend.Application.Articles.Queries;

using FluentValidation;
using Shared.Resources;

public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
{
    public GetArticleQueryValidator() 
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}