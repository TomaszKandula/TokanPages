using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// Use it when you want to issue an invoice.
/// </summary>
[ExcludeFromCodeCoverage]
public class InvoiceItemBase
{
    /// <summary>
    /// Item Text.
    /// </summary>
    public string ItemText { get; set; } = "";

    /// <summary>
    /// Item Quantity.
    /// </summary>
    public int ItemQuantity { get; set; }

    /// <summary>
    /// Item Quantity Unit.
    /// </summary>
    public string ItemQuantityUnit { get; set; } = "";

    /// <summary>
    /// Item Amount.
    /// </summary>
    public decimal ItemAmount { get; set; }

    /// <summary>
    /// Item Discount Rate.
    /// </summary>
    public decimal? ItemDiscountRate { get; set; }

    /// <summary>
    /// VAT rate.
    /// </summary>
    public decimal? VatRate { get; set; }

    /// <summary>
    /// Currency Code.
    /// </summary>
    public CurrencyCodes CurrencyCode { get; set; }
}