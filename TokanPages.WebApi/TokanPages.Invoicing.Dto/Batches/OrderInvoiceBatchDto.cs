using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// Use it when you want to issue an invoice.
/// </summary>
[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchDto
{
    /// <summary>
    /// User Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Company Id.
    /// </summary>
    public Guid UserCompanyId { get; set; }

    /// <summary>
    /// User Bank Account Id.
    /// </summary>
    public Guid UserBankAccountId { get; set; }

    /// <summary>
    /// Order Details.
    /// </summary>
    public IEnumerable<OrderDetailBase<InvoiceItemBase>> OrderDetails { get; set; } = new List<OrderDetailBase<InvoiceItemBase>>();
}