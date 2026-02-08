using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "IssuedInvoices")]
public class IssuedInvoice : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required string InvoiceNumber { get; set; }

    public required byte[] InvoiceData { get; set; }

    public required string ContentType { get; set; }

    public required DateTime GeneratedAt { get; set; }
}