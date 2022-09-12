namespace TokanPages.Backend.Application.Handlers.Commands.Users;

using System;
using FluentValidation;
using Shared.Resources;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        When(command => command.Id != null, () =>
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.ResetId != null, () =>
        {
            RuleFor(command => command.ResetId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        When(command => command.OldPassword != null, () =>
        {
            RuleFor(command => command.OldPassword)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });

        RuleFor(command => command.NewPassword)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(100)
            .WithErrorCode(nameof(ValidationCodes.PASSWORD_TOO_LONG))
            .WithMessage(ValidationCodes.PASSWORD_TOO_LONG);
    }
}