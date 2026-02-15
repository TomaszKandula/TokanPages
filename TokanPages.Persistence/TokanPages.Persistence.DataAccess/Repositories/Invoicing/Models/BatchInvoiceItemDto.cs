using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceItemDto
{
    public required Guid BatchInvoiceId { get; init; }

    public required string ItemText { get; init; }

    public required int ItemQuantity { get; init; }

    public required string ItemQuantityUnit { get; init; }

    public required decimal ItemAmount { get; init; }

    public decimal? ItemDiscountRate { get; init; }

    public required decimal ValueAmount { get; init; }

    public decimal? VatRate { get; init; }

    public required decimal GrossAmount { get; init; }

    public required CurrencyCode CurrencyCode { get; init; }
}