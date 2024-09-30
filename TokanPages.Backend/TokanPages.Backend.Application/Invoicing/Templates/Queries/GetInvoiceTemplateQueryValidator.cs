using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplateQueryValidator : AbstractValidator<GetInvoiceTemplateQuery>
{
    public GetInvoiceTemplateQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
            .WithMessage(ValidationCodes.INVALID_GUID_VALUE);
    }
}