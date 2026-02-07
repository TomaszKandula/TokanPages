using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "IssuedInvoices")]
public class IssuedInvoice : Entity<Guid>
{
    public Guid UserId { get; set; }

    public string InvoiceNumber { get; set; }

    public byte[] InvoiceData { get; set; }

    public string ContentType { get; set; }

    public DateTime GeneratedAt { get; set; }
}