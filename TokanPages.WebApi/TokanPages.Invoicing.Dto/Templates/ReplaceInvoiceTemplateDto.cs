using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Invoicing.Dto.Templates;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class ReplaceInvoiceTemplateDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }  = "";

    /// <summary>
    /// 
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}