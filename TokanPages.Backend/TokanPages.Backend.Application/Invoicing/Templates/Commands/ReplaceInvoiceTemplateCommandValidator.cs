using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class ReplaceInvoiceTemplateCommandValidator : AbstractValidator<ReplaceInvoiceTemplateCommand>
{
    public ReplaceInvoiceTemplateCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.INVALID_GUID_VALUE))
            .WithMessage(ValidationCodes.INVALID_GUID_VALUE);

        RuleFor(command => command.Data)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        When(command => command.Data != null, () =>
        {
            RuleFor(command => command.Data)
                .Must(bytes => bytes!.Length <= 4 * 1024 * 1024)
                .WithErrorCode(nameof(ValidationCodes.INVALID_FILE_SIZE))
                .WithMessage(ValidationCodes.INVALID_FILE_SIZE);
        });

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}