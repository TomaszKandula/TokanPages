using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Services.Models;

[ExcludeFromCodeCoverage]
public class Addresses
{
    public string Admin { get; set; } = "";

    public string Contact { get; set; } = "";

    public string ItSupport { get; set; } = "";
}