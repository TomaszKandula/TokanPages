using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplateQueryValidator : AbstractValidator<GetInvoiceTemplateQuery>
{
    public GetInvoiceTemplateQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}