using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Health;

/// <summary>
/// Returns result object from heath check
/// </summary>
[ExcludeFromCodeCoverage]
public class ActionResultDto
{
    /// <summary>
    /// IsSucceeded
    /// </summary>
    public bool IsSucceeded { get; set; }

    /// <summary>
    /// ErrorCode
    /// </summary>
    public string ErrorCode { get; set; } = "";

    /// <summary>
    /// ErrorDesc
    /// </summary>
    public string ErrorDesc { get; set; } = "";

    /// <summary>
    /// InnerMessage
    /// </summary>
    public string InnerMessage { get; set; } = "";
}