using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RetrieveArticleInfoCommandValidator : AbstractValidator<RetrieveArticleInfoCommand>
{
    public RetrieveArticleInfoCommandValidator()
    {
        RuleFor(command => command.ArticleIds)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}