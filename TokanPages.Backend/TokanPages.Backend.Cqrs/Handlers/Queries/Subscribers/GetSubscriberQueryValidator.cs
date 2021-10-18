namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    using FluentValidation;
    using Shared.Resources;

    public class GetSubscriberQueryValidator : AbstractValidator<GetSubscriberQuery>
    {
        public GetSubscriberQueryValidator() 
        {
            RuleFor(query => query.Id)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}