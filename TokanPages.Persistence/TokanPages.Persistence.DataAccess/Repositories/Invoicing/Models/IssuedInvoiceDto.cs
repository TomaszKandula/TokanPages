using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class IssuedInvoiceDto
{
    public required Guid UserId { get; init; } 

    public required string InvoiceNumber { get; init; }

    public required byte[] InvoiceData { get; init; }
}