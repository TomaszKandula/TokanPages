using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoiceItems")]
public class BatchInvoiceItem : Entity<Guid>
{
    public Guid BatchInvoiceId { get; set; }

    public string ItemText { get; set; }

    public int ItemQuantity { get; set; }

    public string ItemQuantityUnit { get; set; }

    public decimal ItemAmount { get; set; }

    public decimal? ItemDiscountRate { get; set; }

    public decimal ValueAmount { get; set; }

    public decimal? VatRate { get; set; }

    public decimal GrossAmount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }
}