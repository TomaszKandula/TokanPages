using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Invoicing.Dto.Templates;

/// <summary>
/// Use it when you want to add new invoice template.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddInvoiceTemplateDto
{
    /// <summary>
    /// Template name.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Template data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}