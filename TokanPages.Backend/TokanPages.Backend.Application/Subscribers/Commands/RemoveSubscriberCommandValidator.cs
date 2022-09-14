using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class RemoveSubscriberCommandValidator : AbstractValidator<RemoveSubscriberCommand>
{
    public RemoveSubscriberCommandValidator() 
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}