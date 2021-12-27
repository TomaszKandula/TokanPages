namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

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