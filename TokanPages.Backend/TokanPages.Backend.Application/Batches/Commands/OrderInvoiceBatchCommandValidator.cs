using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Application.Batches.Commands;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchCommandValidator : AbstractValidator<OrderInvoiceBatchCommand>
{
    public OrderInvoiceBatchCommandValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(request => request.UserCompanyId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(request => request.UserBankAccountId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}