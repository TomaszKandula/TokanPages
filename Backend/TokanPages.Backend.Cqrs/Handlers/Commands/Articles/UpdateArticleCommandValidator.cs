using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(AField => AField.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            When(AField => AField.Title != null, () => 
            {
                RuleFor(AField => AField.Title)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .MaximumLength(255)
                    .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
                    .WithMessage(ValidationCodes.TITLE_TOO_LONG);
            });

            When(AField => AField.Description !=null, () => 
            {
                RuleFor(AField => AField.Description)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .MaximumLength(255)
                    .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                    .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);
            });

            RuleFor(AField => AField.AddToLikes)
                .GreaterThan(-1)
                .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                .WithMessage(ValidationCodes.LESS_THAN_ZERO);
        }
    }
}
