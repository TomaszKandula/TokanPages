using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceDataDto : FileResultDto
{
    public string Number { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; }
}