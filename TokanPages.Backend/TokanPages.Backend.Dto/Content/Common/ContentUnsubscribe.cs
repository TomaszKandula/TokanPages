namespace TokanPages.Backend.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// ContentUnsubscribe
/// </summary>
[ExcludeFromCodeCoverage]
public class ContentUnsubscribe
{
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
    /// Text3
    /// </summary>
    [JsonProperty("text3")]
    public string Text3 { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";
}