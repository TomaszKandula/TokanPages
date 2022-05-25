namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ArticleFeaturesDto : BaseClass
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }

    [JsonProperty("text1")]
    public string Text1 { get; set; }

    [JsonProperty("text2")]
    public string Text2 { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("image1")]
    public string Image1 { get; set; }

    [JsonProperty("image2")]
    public string Image2 { get; set; }

    [JsonProperty("image3")]
    public string Image3 { get; set; }

    [JsonProperty("image4")]
    public string Image4 { get; set; }
}