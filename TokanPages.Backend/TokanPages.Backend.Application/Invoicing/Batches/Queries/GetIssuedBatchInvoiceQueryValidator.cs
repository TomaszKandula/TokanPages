using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetIssuedBatchInvoiceQueryValidator : AbstractValidator<GetIssuedBatchInvoiceQuery>
{
    public GetIssuedBatchInvoiceQueryValidator()
    {
        RuleFor(request => request.InvoiceNumber)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}