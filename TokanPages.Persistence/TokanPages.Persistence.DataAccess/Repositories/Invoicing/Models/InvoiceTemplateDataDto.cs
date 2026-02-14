using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateDataDto : FileResultDto
{
    public required string Description { get; init; }
}