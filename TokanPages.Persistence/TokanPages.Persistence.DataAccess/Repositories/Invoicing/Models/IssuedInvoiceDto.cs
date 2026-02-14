using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class IssuedInvoiceDto
{
    public Guid UserId { get; init; } 

    public string InvoiceNumber { get; init; } = string.Empty;

    public byte[] InvoiceData { get; init; } = Array.Empty<byte>();
}