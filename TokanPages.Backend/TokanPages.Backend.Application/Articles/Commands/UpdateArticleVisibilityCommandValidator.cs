namespace TokanPages.Backend.Application.Articles.Commands;

using FluentValidation;
using Shared.Resources;

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