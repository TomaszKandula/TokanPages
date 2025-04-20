using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// CPU details.
/// </summary>
[ExcludeFromCodeCoverage]
public class Cpu
{
    /// <summary>
    /// CPU architecture.
    /// </summary>
    public string? Architecture { get; set; }
}