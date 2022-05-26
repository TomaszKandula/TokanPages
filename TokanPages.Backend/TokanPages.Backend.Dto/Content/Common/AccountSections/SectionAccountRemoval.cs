namespace TokanPages.Backend.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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
    public string Caption { get; set; }

    /// <summary>
    /// WarningText
    /// </summary>
    [JsonProperty("warningText")]
    public string WarningText { get; set; }

    /// <summary>
    /// DeleteButtonText
    /// </summary>
    [JsonProperty("deleteButtonText")]
    public string DeleteButtonText { get; set; }
}