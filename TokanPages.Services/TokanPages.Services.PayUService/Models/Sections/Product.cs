using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Product
{
    public string? Name { get; set; }

    public string? UnitPrice { get; set; }

    public string? Quantity { get; set; }
}