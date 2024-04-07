using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.TemplateService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateData : FileResult
{
    public string Description { get; set; } = "";
}