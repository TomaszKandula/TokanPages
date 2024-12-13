using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.SpaCachingService.Models;

[ExcludeFromCodeCoverage]
public class RequestCacheProcessing
{
    public Guid MessageId { get; set; }

    public string? TargetEnv { get; set; }

    public string GetUrl { get; set; } = "";

    public string PostUrl { get; set; } = "";

    public string[]? Files { get; set; }

    public List<RoutePath> Paths { get; set; } = new();
}