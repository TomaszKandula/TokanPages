using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{

    public class RemoveSubscriberCommandValidator : AbstractValidator<RemoveSubscriberCommand>
    {

        public RemoveSubscriberCommandValidator() 
        {

            RuleFor(Field => Field.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

        }

    }

}
