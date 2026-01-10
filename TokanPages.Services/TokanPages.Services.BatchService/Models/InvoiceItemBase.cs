using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceItemBase
{
    public string ItemText { get; set; } = "";

    public int ItemQuantity { get; set; }

    public string ItemQuantityUnit { get; set; } = "";

    public decimal ItemAmount { get; set; }

    public decimal? ItemDiscountRate { get; set; }

    public decimal? VatRate { get; set; }

    public Backend.Domain.Enums.CurrencyCode CurrencyCode { get; set; }
}