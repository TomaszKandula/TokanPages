using TokanPages.Backend.Shared.Resources;
using FluentValidation;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderTransactionsQueryValidator : AbstractValidator<GetOrderTransactionsQuery>
{
    public GetOrderTransactionsQueryValidator()
    {
        RuleFor(query => query.OrderId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}