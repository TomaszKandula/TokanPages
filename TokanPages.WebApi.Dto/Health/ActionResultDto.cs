using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Health;

/// <summary>
/// Returns result object from heath check.
/// </summary>
[ExcludeFromCodeCoverage]
public class ActionResultDto
{
    /// <summary>
    /// Success flag.
    /// </summary>
    public bool IsSucceeded { get; set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public string ErrorCode { get; set; } = "";

    /// <summary>
    /// Error description.
    /// </summary>
    public string ErrorDesc { get; set; } = "";

    /// <summary>
    /// Error inner message.
    /// </summary>
    public string InnerMessage { get; set; } = "";
}