using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class RemoveArticleCommandValidator : AbstractValidator<RemoveArticleCommand>
    {
        public RemoveArticleCommandValidator() 
        {
            RuleFor(Field => Field.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}
