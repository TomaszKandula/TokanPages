namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
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
}