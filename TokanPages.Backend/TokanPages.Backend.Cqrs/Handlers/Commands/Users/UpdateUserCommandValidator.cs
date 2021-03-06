﻿using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(AField => AField.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(AField => AField.UserAlias)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.USERALIAS_TOO_LONG))
                .WithMessage(ValidationCodes.USERALIAS_TOO_LONG);

            RuleFor(AField => AField.FirstName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.FIRST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.FIRST_NAME_TOO_LONG);

            RuleFor(AField => AField.LastName)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.LAST_NAME_TOO_LONG))
                .WithMessage(ValidationCodes.LAST_NAME_TOO_LONG);

            RuleFor(AField => AField.EmailAddress)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
        }
    }
}
