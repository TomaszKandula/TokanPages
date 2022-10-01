using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common;

/// <summary>
/// Section
/// </summary>
[ExcludeFromCodeCoverage]
public class Section
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
    public string Type { get; set; } = "";
        
    /// <summary>
    /// Value
    /// </summary>
    [JsonProperty("value")]
    public dynamic? Value { get; set; }
        
    /// <summary>
    /// Prop
    /// </summary>
    [JsonProperty("prop")]
    public string Prop { get; set; } = "";
        
    /// <summary>
    /// Text
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; } = "";
}