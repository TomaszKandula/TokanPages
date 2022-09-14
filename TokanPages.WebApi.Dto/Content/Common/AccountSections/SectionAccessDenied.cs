using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common.AccountSections;

/// <summary>
/// SectionAccessDenied
/// </summary>
[ExcludeFromCodeCoverage]
public class SectionAccessDenied
{
    /// <summary>
    /// AccessDeniedCaption
    /// </summary>
    [JsonProperty("accessDeniedCaption")]
    public string AccessDeniedCaption { get; set; } = "";

    /// <summary>
    /// AccessDeniedPrompt
    /// </summary>
    [JsonProperty("accessDeniedPrompt")]
    public string AccessDeniedPrompt { get; set; } = "";

    /// <summary>
    /// HomeButtonText
    /// </summary>
    [JsonProperty("homeButtonText")]
    public string HomeButtonText { get; set; } = "";
}