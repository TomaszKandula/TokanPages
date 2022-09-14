using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleVisibilityCommandValidator : AbstractValidator<UpdateArticleVisibilityCommand>
{
    public UpdateArticleVisibilityCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}