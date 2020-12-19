﻿using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{

    public class AddArticleCommandValidator : AbstractValidator<AddArticleCommand>
    {

        public AddArticleCommandValidator() 
        {

            RuleFor(Field => Field.Title)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
                .WithMessage(ValidationCodes.TITLE_TOO_LONG);

            RuleFor(Field => Field.Desc)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);

            RuleFor(Field => Field.Text)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

        }

    }

}
