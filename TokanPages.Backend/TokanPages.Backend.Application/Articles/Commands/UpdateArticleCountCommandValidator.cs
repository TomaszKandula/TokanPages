using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Commands;

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