using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.TemplateService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplate
{
    public string TemplateName { get; set; } = "";

    public InvoiceTemplateData InvoiceTemplateData { get; set; } = new();

    public string InvoiceTemplateDescription { get; set; } = "";
}