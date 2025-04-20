using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// Detailed information of a machine that sends API requests.
/// </summary>
[ExcludeFromCodeCoverage]
public class Parsed
{
    /// <summary>
    /// Browser details.
    /// </summary>
    public Browser Browser { get; set; } = new();

    /// <summary>
    /// CPU details.
    /// </summary>
    public Cpu Cpu { get; set; } = new();

    /// <summary>
    /// Device details.
    /// </summary>
    public Device Device { get; set; } = new();

    /// <summary>
    /// Browser engine details.
    /// </summary>
    public Engine Engine { get; set; } = new();

    /// <summary>
    /// Operating System details.
    /// </summary>
    public Os Os { get; set; } = new();
}