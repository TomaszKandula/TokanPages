namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TestimonialsDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("subtitle")]
    public string Subtitle { get; set; }

    [JsonProperty("photo1")]
    public string Photo1 { get; set; }

    [JsonProperty("name1")]
    public string Name1 { get; set; }

    [JsonProperty("occupation1")]
    public string Occupation1 { get; set; }

    [JsonProperty("text1")]
    public string Text1 { get; set; }
        
    [JsonProperty("photo2")]
    public string Photo2 { get; set; }

    [JsonProperty("name2")]
    public string Name2 { get; set; }

    [JsonProperty("occupation2")]
    public string Occupation2 { get; set; }

    [JsonProperty("text2")]
    public string Text2 { get; set; }
        
    [JsonProperty("photo3")]
    public string Photo3 { get; set; }

    [JsonProperty("name3")]
    public string Name3 { get; set; }

    [JsonProperty("occupation3")]
    public string Occupation3 { get; set; }

    [JsonProperty("text3")]
    public string Text3 { get; set; }
}