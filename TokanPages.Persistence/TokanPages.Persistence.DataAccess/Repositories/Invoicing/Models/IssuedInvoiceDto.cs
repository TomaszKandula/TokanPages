using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class IssuedInvoiceDto
{
    public Guid UserId { get; set; } 

    public string InvoiceNumber { get; set; } = string.Empty;

    public byte[] InvoiceData { get; set; } = Array.Empty<byte>();
}