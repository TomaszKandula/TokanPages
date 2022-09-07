namespace TokanPages.WebApi.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// ArticleFeaturesDto
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticleFeaturesDto : BaseClass
{
    /// <summary>
    /// Title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = "";

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
    [JsonProperty("action")]
    public Link Action { get; set; } = new();

    /// <summary>
    /// Image1
    /// </summary>
    [JsonProperty("image1")]
    public string Image1 { get; set; } = "";

    /// <summary>
    /// Image2
    /// </summary>
    [JsonProperty("image2")]
    public string Image2 { get; set; } = "";

    /// <summary>
    /// Image3
    /// </summary>
    [JsonProperty("image3")]
    public string Image3 { get; set; } = "";

    /// <summary>
    /// Image4
    /// </summary>
    [JsonProperty("image4")]
    public string Image4 { get; set; } = "";
}