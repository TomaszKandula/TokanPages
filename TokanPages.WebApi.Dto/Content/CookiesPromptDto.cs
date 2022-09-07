namespace TokanPages.WebApi.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// CookiesPromptDto
/// </summary>
[ExcludeFromCodeCoverage]
public class CookiesPromptDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Text
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";

    /// <summary>
    /// Days
    /// </summary>
    [JsonProperty("days")]
    public int Days { get; set; }
}