﻿using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommandValidator : AbstractValidator<UpdateArticleContentCommand>
{
    public UpdateArticleContentCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        When(command => command.Title != null, () => 
        {
            RuleFor(command => command.Title)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.TITLE_TOO_LONG))
                .WithMessage(ValidationCodes.TITLE_TOO_LONG);
        });

        When(command => command.Description !=null, () => 
        {
            RuleFor(command => command.Description)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.DESCRIPTION_TOO_LONG))
                .WithMessage(ValidationCodes.DESCRIPTION_TOO_LONG);
        });
    }
}