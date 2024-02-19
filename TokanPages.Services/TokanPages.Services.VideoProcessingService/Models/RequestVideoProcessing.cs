using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.VideoProcessingService.Models;

[ExcludeFromCodeCoverage]
public class RequestVideoProcessing
{
    public Guid MessageId { get; set; }

    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string? TargetEnv { get; set; }

    public TargetDetails? Details { get; set; }
}