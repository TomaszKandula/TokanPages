namespace TokanPages.Backend.Dto.Content;

using Base;
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
    public string Title { get; set; }

    /// <summary>
    /// Desc
    /// </summary>
    [JsonProperty("desc")]
    public string Desc { get; set; }

    /// <summary>
    /// Text1
    /// </summary>
    [JsonProperty("text1")]
    public string Text1 { get; set; }

    /// <summary>
    /// Text2
    /// </summary>
    [JsonProperty("text2")]
    public string Text2 { get; set; }

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; }

    /// <summary>
    /// Image1
    /// </summary>
    [JsonProperty("image1")]
    public string Image1 { get; set; }

    /// <summary>
    /// Image2
    /// </summary>
    [JsonProperty("image2")]
    public string Image2 { get; set; }

    /// <summary>
    /// Image3
    /// </summary>
    [JsonProperty("image3")]
    public string Image3 { get; set; }

    /// <summary>
    /// Image4
    /// </summary>
    [JsonProperty("image4")]
    public string Image4 { get; set; }
}