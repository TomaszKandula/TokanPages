using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceDataDto : FileResultDto
{
    public required string Number { get; init; }

    public required DateTime GeneratedAt { get; init; }
}