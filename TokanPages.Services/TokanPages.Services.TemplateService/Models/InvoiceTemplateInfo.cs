using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.TemplateService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateInfo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
}