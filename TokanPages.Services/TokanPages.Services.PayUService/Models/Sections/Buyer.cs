using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Buyer
{
    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Language { get; set; }
}