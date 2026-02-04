using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateDto
{
    public string TemplateName { get; set; } = string.Empty;

    public InvoiceTemplateDataDto InvoiceTemplateData { get; set; } = new();

    public string InvoiceTemplateDescription { get; set; } = string.Empty;
}