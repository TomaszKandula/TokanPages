namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class FeaturesDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("header")]
    public string Header { get; set; }

    [JsonProperty("title1")]
    public string Title1 { get; set; }

    [JsonProperty("text1")]
    public string Text1 { get; set; }

    [JsonProperty("title2")]
    public string Title2 { get; set; }

    [JsonProperty("text2")]
    public string Text2 { get; set; }

    [JsonProperty("title3")]
    public string Title3 { get; set; }

    [JsonProperty("text3")]
    public string Text3 { get; set; }

    [JsonProperty("title4")]
    public string Title4 { get; set; }

    [JsonProperty("text4")]
    public string Text4 { get; set; }
}