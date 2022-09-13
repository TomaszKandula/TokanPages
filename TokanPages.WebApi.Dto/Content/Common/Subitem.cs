using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

#nullable enable
namespace TokanPages.WebApi.Dto.Content.Common;

/// <summary>
/// Subitem
/// </summary>
[ExcludeFromCodeCoverage]
public class Subitem
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonProperty("value")]
    public string? Value { get; set; }

    /// <summary>
    /// Link
    /// </summary>
    [JsonProperty("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Enabled
    /// </summary>
    [JsonProperty("enabled")]
    public bool? Enabled { get; set; }
}