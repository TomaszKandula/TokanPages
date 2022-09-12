namespace TokanPages.Backend.Application.Subscribers.Queries;

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