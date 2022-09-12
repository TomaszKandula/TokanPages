namespace TokanPages.Backend.Application.Articles.Commands;

using FluentValidation;
using Shared.Resources;

public class UpdateArticleCountCommandValidator : AbstractValidator<UpdateArticleCountCommand>
{
    public UpdateArticleCountCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}