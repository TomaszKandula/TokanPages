using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class RemoveInvoiceTemplateCommandValidator : AbstractValidator<RemoveInvoiceTemplateCommand>
{
    public RemoveInvoiceTemplateCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}