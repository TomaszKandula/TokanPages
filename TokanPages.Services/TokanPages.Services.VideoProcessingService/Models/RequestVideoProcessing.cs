using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.VideoProcessingService.Models;

[ExcludeFromCodeCoverage]
public class RequestVideoProcessing
{
    public Guid MessageId { get; init; }

    public Guid TicketId { get; init; }

    public string? TargetEnv { get; init; }

    public TargetDetails? Details { get; init; }
}