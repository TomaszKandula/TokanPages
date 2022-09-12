namespace TokanPages.Backend.Application.Users.Commands;

using System;
using FluentValidation;
using Shared.Resources;

public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator() 
    {
        When(command => command.Id != null, () =>
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });
    }
}