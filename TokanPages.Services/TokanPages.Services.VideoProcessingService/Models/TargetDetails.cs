using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Services.VideoProcessingService.Models;

[ExcludeFromCodeCoverage]
public class TargetDetails
{
    public ProcessingTarget Target { get; set; }

    public Guid? EntityId { get; set; }

    public bool? ShouldCompactVideo { get; set; }
}