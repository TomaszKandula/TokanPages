using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Templates.Queries;

public class GetInvoiceTemplateQueryValidator : AbstractValidator<GetInvoiceTemplateQuery>
{
    public GetInvoiceTemplateQueryValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}