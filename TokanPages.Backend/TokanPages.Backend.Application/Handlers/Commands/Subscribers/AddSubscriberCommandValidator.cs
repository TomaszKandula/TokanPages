namespace TokanPages.Backend.Application.Handlers.Commands.Subscribers;

using FluentValidation;
using Shared.Resources;

public class AddSubscriberCommandValidator : AbstractValidator<AddSubscriberCommand>
{
    public AddSubscriberCommandValidator() 
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);
    }
}