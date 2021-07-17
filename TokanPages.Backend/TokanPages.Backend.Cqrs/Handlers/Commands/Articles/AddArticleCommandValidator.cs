namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using FluentValidation;
    using Shared.Resources;

    public class AddArticleCommandValidator : AbstractValidator<AddArticleCommand>
    {
        public AddArticleCommandValidator() 
        {
            RuleFor(AField => AField.Title)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
                .WithMessage(ValidationCodes.TITLE_TOO_LONG);

            RuleFor(AField => AField.Description)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);

            RuleFor(AField => AField.TextToUpload)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(AField => AField.ImageToUpload)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}