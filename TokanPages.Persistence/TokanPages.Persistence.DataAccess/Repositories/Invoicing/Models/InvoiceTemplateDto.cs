using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateDto
{
    public required string TemplateName { get; init; }

    public required InvoiceTemplateDataDto InvoiceTemplateData { get; init; }

    public required string InvoiceTemplateDescription { get; init; }
}