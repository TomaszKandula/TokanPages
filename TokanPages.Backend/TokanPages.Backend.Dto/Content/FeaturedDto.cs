namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class FeaturedDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("title1")]
    public string Title1 { get; set; }

    [JsonProperty("subtitle1")]
    public string Subtitle1 { get; set; }

    [JsonProperty("link1")]
    public string Link1 { get; set; }

    [JsonProperty("image1")]
    public string Image1 { get; set; }

    [JsonProperty("title2")]
    public string Title2 { get; set; }

    [JsonProperty("subtitle2")]
    public string Subtitle2 { get; set; }

    [JsonProperty("link2")]
    public string Link2 { get; set; }

    [JsonProperty("image2")]
    public string Image2 { get; set; }

    [JsonProperty("title3")]
    public string Title3 { get; set; }

    [JsonProperty("subtitle3")]
    public string Subtitle3 { get; set; }

    [JsonProperty("link3")]
    public string Link3 { get; set; }

    [JsonProperty("image3")]
    public string Image3 { get; set; }
}