namespace TokanPages.Backend.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Language item
/// </summary>
[ExcludeFromCodeCoverage]
public class LanguageItem
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = "";

    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Default flag
    /// </summary>
    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }
}