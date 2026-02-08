using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoiceItems")]
public class BatchInvoiceItem : Entity<Guid>
{
    public required Guid BatchInvoiceId { get; set; }

    public required string ItemText { get; set; }

    public required int ItemQuantity { get; set; }

    public required string ItemQuantityUnit { get; set; }

    public required decimal ItemAmount { get; set; }

    public decimal? ItemDiscountRate { get; set; }

    public required decimal ValueAmount { get; set; }

    public decimal? VatRate { get; set; }

    public required decimal GrossAmount { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }
}