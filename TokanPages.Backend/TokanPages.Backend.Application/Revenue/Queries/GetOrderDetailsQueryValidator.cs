using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
{ 
    public GetOrderDetailsQueryValidator()
    {
        RuleFor(query => query.OrderId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}