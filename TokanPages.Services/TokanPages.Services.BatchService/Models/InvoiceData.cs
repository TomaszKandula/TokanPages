using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class InvoiceData : FileResult
{
    public string Number { get; set; } = "";

    public DateTime GeneratedAt { get; set; }
}