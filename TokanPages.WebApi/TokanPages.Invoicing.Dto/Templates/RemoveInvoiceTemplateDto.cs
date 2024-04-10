using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Invoicing.Dto.Templates;

/// <summary>
/// Use it when you want to remove an existing invoice template.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveInvoiceTemplateDto
{
    /// <summary>
    /// Template ID.
    /// </summary>
    public Guid Id { get; set; }        
}