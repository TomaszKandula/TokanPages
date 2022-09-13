using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Commands;

public class AddArticleCommandValidator : AbstractValidator<AddArticleCommand>
{
    public AddArticleCommandValidator() 
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
            .WithMessage(ValidationCodes.TITLE_TOO_LONG);

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
            .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);

        RuleFor(command => command.TextToUpload)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.ImageToUpload)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}