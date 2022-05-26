namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class FeaturedDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; }

    /// <summary>
    /// Text
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Title1
    /// </summary>
    [JsonProperty("title1")]
    public string Title1 { get; set; }

    /// <summary>
    /// Subtitle1
    /// </summary>
    [JsonProperty("subtitle1")]
    public string Subtitle1 { get; set; }

    /// <summary>
    /// Link1
    /// </summary>
    [JsonProperty("link1")]
    public string Link1 { get; set; }

    /// <summary>
    /// Image1
    /// </summary>
    [JsonProperty("image1")]
    public string Image1 { get; set; }

    /// <summary>
    /// Title2
    /// </summary>
    [JsonProperty("title2")]
    public string Title2 { get; set; }

    /// <summary>
    /// Subtitle2
    /// </summary>
    [JsonProperty("subtitle2")]
    public string Subtitle2 { get; set; }

    /// <summary>
    /// Link2
    /// </summary>
    [JsonProperty("link2")]
    public string Link2 { get; set; }

    /// <summary>
    /// Image2
    /// </summary>
    [JsonProperty("image2")]
    public string Image2 { get; set; }

    /// <summary>
    /// Title3
    /// </summary>
    [JsonProperty("title3")]
    public string Title3 { get; set; }

    /// <summary>
    /// Subtitle3
    /// </summary>
    [JsonProperty("subtitle3")]
    public string Subtitle3 { get; set; }

    /// <summary>
    /// Link3
    /// </summary>
    [JsonProperty("link3")]
    public string Link3 { get; set; }

    /// <summary>
    /// Image3
    /// </summary>
    [JsonProperty("image3")]
    public string Image3 { get; set; }
}