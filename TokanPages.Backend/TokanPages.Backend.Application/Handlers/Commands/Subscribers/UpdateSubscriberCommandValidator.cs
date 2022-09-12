namespace TokanPages.Backend.Application.Handlers.Commands.Subscribers;

using FluentValidation;
using Shared.Resources;

public class UpdateSubscriberCommandValidator : AbstractValidator<UpdateSubscriberCommand>
{
    public UpdateSubscriberCommandValidator() 
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
            .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

        When(command => command.Count != null, () => 
        {
            RuleFor(command => command.Count)
                .GreaterThan(-1)
                .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                .WithMessage(ValidationCodes.LESS_THAN_ZERO);
        });
    }
}