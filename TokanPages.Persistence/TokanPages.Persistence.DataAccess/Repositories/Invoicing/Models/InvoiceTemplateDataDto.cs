using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateDataDto : FileResultDto
{
    public string Description { get; set; } = string.Empty;
}