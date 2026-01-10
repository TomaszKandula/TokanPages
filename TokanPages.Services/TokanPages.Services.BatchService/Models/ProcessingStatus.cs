using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class ProcessingStatus
{
    public TimeSpan BatchProcessingTime { get; set; }

    public Backend.Domain.Enums.ProcessingStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}