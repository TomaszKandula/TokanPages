using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common;

/// <summary>
/// ContentActivation
/// </summary>
[ExcludeFromCodeCoverage]
public class ContentActivation
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = "";
        
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";
        
    /// <summary>
    /// Text1
    /// </summary>
    [JsonProperty("text1")]
    public string Text1 { get; set; } = "";
        
    /// <summary>
    /// Text2
    /// </summary>
    [JsonProperty("text2")]
    public string Text2 { get; set; } = "";
        
    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";
}