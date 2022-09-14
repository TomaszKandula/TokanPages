using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// TestimonialsDto
/// </summary>
[ExcludeFromCodeCoverage]
public class TestimonialsDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Subtitle
    /// </summary>
    [JsonProperty("subtitle")]
    public string Subtitle { get; set; } = "";

    /// <summary>
    /// Photo1
    /// </summary>
    [JsonProperty("photo1")]
    public string Photo1 { get; set; } = "";

    /// <summary>
    /// Name1
    /// </summary>
    [JsonProperty("name1")]
    public string Name1 { get; set; } = "";

    /// <summary>
    /// Occupation1
    /// </summary>
    [JsonProperty("occupation1")]
    public string Occupation1 { get; set; } = "";

    /// <summary>
    /// Text1
    /// </summary>
    [JsonProperty("text1")]
    public string Text1 { get; set; } = "";
        
    /// <summary>
    /// Photo2
    /// </summary>
    [JsonProperty("photo2")]
    public string Photo2 { get; set; } = "";

    /// <summary>
    /// Name2
    /// </summary>
    [JsonProperty("name2")]
    public string Name2 { get; set; } = "";

    /// <summary>
    /// Occupation2
    /// </summary>
    [JsonProperty("occupation2")]
    public string Occupation2 { get; set; } = "";

    /// <summary>
    /// Text2
    /// </summary>
    [JsonProperty("text2")]
    public string Text2 { get; set; } = "";
        
    /// <summary>
    /// Photo3
    /// </summary>
    [JsonProperty("photo3")]
    public string Photo3 { get; set; } = "";

    /// <summary>
    /// Name3
    /// </summary>
    [JsonProperty("name3")]
    public string Name3 { get; set; } = "";

    /// <summary>
    /// Occupation3
    /// </summary>
    [JsonProperty("occupation3")]
    public string Occupation3 { get; set; } = "";

    /// <summary>
    /// Text3
    /// </summary>
    [JsonProperty("text3")]
    public string Text3 { get; set; } = "";
}