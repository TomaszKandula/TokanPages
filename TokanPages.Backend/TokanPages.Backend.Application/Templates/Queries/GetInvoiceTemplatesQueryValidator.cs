using FluentValidation;

namespace TokanPages.Backend.Application.Templates.Queries;

public class GetInvoiceTemplatesQueryValidator : AbstractValidator<GetInvoiceTemplatesQuery>
{
    public GetInvoiceTemplatesQueryValidator()
    {
        // RuleFor(request => request.PrivateKey)
        //     .NotEmpty()
        //     .WithErrorCode(nameof(ValidationCodes.REQUIRED))
        //     .WithMessage(ValidationCodes.REQUIRED);
    }
}