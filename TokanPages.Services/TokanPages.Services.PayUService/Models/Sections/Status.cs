using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Status
{
    public string? StatusCode { get; set; }

    public string? StatusDesc { get; set; }
}