namespace TokanPages.Backend.Application.Content.Cached.Commands.Models;

public class RequestProcessing
{
    public Guid MessageId { get; set; }

    public string? TargetEnv { get; set; }

    public string GetUrl { get; set; } = "";

    public string PostUrl { get; set; } = "";

    public string[]? Files { get; set; }

    public IEnumerable<RoutePath>? Paths { get; set; }
}