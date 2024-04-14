using System.Diagnostics.CodeAnalysis;
using MediatR;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Batches.Commands;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchCommand : IRequest<OrderInvoiceBatchCommandResult>
{
    public Guid UserId { get; set; }

    public Guid UserCompanyId { get; set; }

    public Guid UserBankAccountId { get; set; }

    public IEnumerable<OrderDetailBase<InvoiceItemBase>> OrderDetails { get; set; } = new List<OrderDetailBase<InvoiceItemBase>>();
}