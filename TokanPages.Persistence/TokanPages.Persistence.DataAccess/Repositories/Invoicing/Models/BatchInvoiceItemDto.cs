using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceItemDto
{
    public Guid BatchInvoiceId { get; set; }

    public string ItemText { get; set; } = string.Empty;

    public int ItemQuantity { get; set; }

    public string ItemQuantityUnit { get; set; } = string.Empty;

    public decimal ItemAmount { get; set; }

    public decimal? ItemDiscountRate { get; set; }

    public decimal ValueAmount { get; set; }

    public decimal? VatRate { get; set; }

    public decimal GrossAmount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }
}