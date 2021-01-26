using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(Field => Field.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            When(Field => Field.Title != null, () => 
            {
                RuleFor(Field => Field.Title)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .MaximumLength(255)
                    .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
                    .WithMessage(ValidationCodes.TITLE_TOO_LONG);
            });

            When(Field => Field.Description !=null, () => 
            {
                RuleFor(Field => Field.Description)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .MaximumLength(255)
                    .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                    .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);
            });

            When(Field => Field.Likes != null, () => 
            {
                RuleFor(Field => Field.Likes)
                    .GreaterThan(-1)
                    .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                    .WithMessage(ValidationCodes.LESS_THAN_ZERO);
            });

            When(Field => Field.ReadCount != null, () =>
            {
                RuleFor(Field => Field.ReadCount)
                    .GreaterThan(-1)
                    .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                    .WithMessage(ValidationCodes.LESS_THAN_ZERO);
            });
        }
    }
}
