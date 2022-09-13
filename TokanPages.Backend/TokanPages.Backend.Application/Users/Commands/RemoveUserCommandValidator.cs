using System;
using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Users.Commands;

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