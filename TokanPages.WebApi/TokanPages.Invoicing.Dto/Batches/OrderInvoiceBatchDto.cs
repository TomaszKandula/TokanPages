using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class OrderInvoiceBatchDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid UserCompanyId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid UserBankAccountId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<OrderDetailBase<InvoiceItemBase>> OrderDetails { get; set; } = new List<OrderDetailBase<InvoiceItemBase>>();
}