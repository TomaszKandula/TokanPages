using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.SpaCachingService.Models;

[ExcludeFromCodeCoverage]
public class RoutePath
{
    public string Url { get; set; } = "";

    public string Name { get; set; } = "";
}