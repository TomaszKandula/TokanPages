using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Templates.Queries.Models;

[ExcludeFromCodeCoverage]
public class InvoiceTemplateInfo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
}