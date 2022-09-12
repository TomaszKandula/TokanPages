namespace TokanPages.Backend.Application.Articles.Commands;

using FluentValidation;
using Shared.Resources;

public class RemoveArticleCommandValidator : AbstractValidator<RemoveArticleCommand>
{
    public RemoveArticleCommandValidator() 
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}