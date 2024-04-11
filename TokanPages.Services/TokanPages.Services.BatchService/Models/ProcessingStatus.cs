using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class ProcessingStatus
{
    public TimeSpan BatchProcessingTime { get; set; }

    public ProcessingStatuses Status { get; set; }

    public DateTime CreatedAt { get; set; }
}