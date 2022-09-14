using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common.AccountSections;

/// <summary>
/// SectionAccountRemoval
/// </summary>
[ExcludeFromCodeCoverage]
public class SectionAccountRemoval
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// WarningText
    /// </summary>
    [JsonProperty("warningText")]
    public string WarningText { get; set; } = "";

    /// <summary>
    /// DeleteButtonText
    /// </summary>
    [JsonProperty("deleteButtonText")]
    public string DeleteButtonText { get; set; } = "";
}