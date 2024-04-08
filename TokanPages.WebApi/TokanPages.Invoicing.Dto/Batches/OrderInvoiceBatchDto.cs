using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Invoicing.Dto.Batches;

[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchDto
{
    public Guid UserId { get; set; }

    public Guid UserCompanyId { get; set; }

    public Guid UserBankAccountId { get; set; }

    public IEnumerable<OrderDetailBase<InvoiceItemBase>> OrderDetails { get; set; } = new List<OrderDetailBase<InvoiceItemBase>>();
}