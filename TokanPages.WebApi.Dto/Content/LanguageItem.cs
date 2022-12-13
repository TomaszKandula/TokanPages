using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// Language item.
/// </summary>
[ExcludeFromCodeCoverage]
public class LanguageItem
{
    /// <summary>
    /// Identification.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = "";

    /// <summary>
    /// Name.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Default flag.
    /// </summary>
    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }
}