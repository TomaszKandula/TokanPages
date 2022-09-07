namespace TokanPages.WebApi.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// FeaturesDto
/// </summary>
[ExcludeFromCodeCoverage]
public class FeaturesDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Header
    /// </summary>
    [JsonProperty("header")]
    public string Header { get; set; } = "";

    /// <summary>
    /// Title1
    /// </summary>
    [JsonProperty("title1")]
    public string Title1 { get; set; } = "";

    /// <summary>
    /// Text1
    /// </summary>
    [JsonProperty("text1")]
    public string Text1 { get; set; } = "";

    /// <summary>
    /// Title2
    /// </summary>
    [JsonProperty("title2")]
    public string Title2 { get; set; } = "";

    /// <summary>
    /// Text2
    /// </summary>
    [JsonProperty("text2")]
    public string Text2 { get; set; } = "";

    /// <summary>
    /// Title3
    /// </summary>
    [JsonProperty("title3")]
    public string Title3 { get; set; } = "";

    /// <summary>
    /// Text3
    /// </summary>
    [JsonProperty("text3")]
    public string Text3 { get; set; } = "";

    /// <summary>
    /// Title4
    /// </summary>
    [JsonProperty("title4")]
    public string Title4 { get; set; } = "";

    /// <summary>
    /// Text4
    /// </summary>
    [JsonProperty("text4")]
    public string Text4 { get; set; } = "";
}