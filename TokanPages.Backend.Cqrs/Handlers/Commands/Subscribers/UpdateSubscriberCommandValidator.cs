using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers
{
    public class UpdateSubscriberCommandValidator : AbstractValidator<UpdateSubscriberCommand>
    {
        public UpdateSubscriberCommandValidator() 
        {
            RuleFor(AField => AField.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(AField => AField.Email)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .MaximumLength(255)
                .WithErrorCode(nameof(ValidationCodes.EMAIL_TOO_LONG))
                .WithMessage(ValidationCodes.EMAIL_TOO_LONG);

            When(AField => AField.Count != null, () => 
            {
                RuleFor(AField => AField.Count)
                    .GreaterThan(-1)
                    .WithErrorCode(nameof(ValidationCodes.LESS_THAN_ZERO))
                    .WithMessage(ValidationCodes.LESS_THAN_ZERO);
            });
        }
    }
}
