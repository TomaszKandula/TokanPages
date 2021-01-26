using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetSubscriberQueryValidator : AbstractValidator<GetSubscriberQuery>
    {
        public GetSubscriberQueryValidator() 
        {
            RuleFor(Field => Field.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}
