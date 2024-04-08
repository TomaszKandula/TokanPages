using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class InvoiceItemBase
{
    /// <summary>
    /// 
    /// </summary>
    public string ItemText { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public int ItemQuantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ItemQuantityUnit { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public decimal ItemAmount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? ItemDiscountRate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? VatRate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CurrencyCodes CurrencyCode { get; set; }
}