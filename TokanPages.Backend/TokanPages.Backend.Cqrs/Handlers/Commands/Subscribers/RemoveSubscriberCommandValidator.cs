namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

using FluentValidation;
using Shared.Resources;

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