using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceItemDto
{
    public Guid BatchInvoiceId { get; init; }

    public string ItemText { get; init; } = string.Empty;

    public int ItemQuantity { get; init; }

    public string ItemQuantityUnit { get; init; } = string.Empty;

    public decimal ItemAmount { get; init; }

    public decimal? ItemDiscountRate { get; init; }

    public decimal ValueAmount { get; init; }

    public decimal? VatRate { get; init; }

    public decimal GrossAmount { get; init; }

    public CurrencyCode CurrencyCode { get; init; }
}