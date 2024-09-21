using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.VideoConverterService.Models;

[ExcludeFromCodeCoverage]
public class ProcessingWarning
{
    public string? ErrorMessage { get; set; }

    public string? ErrorInnerMessage { get; set; }

    public string? Message { get; set; }
}