using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchCommandValidator : AbstractValidator<OrderInvoiceBatchCommand>
{
    public OrderInvoiceBatchCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.UserCompanyId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.UserBankAccountId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}