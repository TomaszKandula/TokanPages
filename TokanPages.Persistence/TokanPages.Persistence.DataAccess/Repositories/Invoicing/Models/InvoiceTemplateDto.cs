using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateDto
{
    public string TemplateName { get; init; } = string.Empty;

    public InvoiceTemplateDataDto InvoiceTemplateData { get; init; } = new();

    public string InvoiceTemplateDescription { get; init; } = string.Empty;
}