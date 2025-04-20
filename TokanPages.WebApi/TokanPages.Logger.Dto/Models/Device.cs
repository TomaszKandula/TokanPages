using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// Device details.
/// </summary>
[ExcludeFromCodeCoverage]
public class Device
{
    /// <summary>
    /// Device model.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Device type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Device vendor.
    /// </summary>
    public string? Vendor { get; set; }
}