namespace TokanPages.WebApi.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Link
/// </summary>
[ExcludeFromCodeCoverage]
public class Link
{
    /// <summary>
    /// Link description
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; } = "";

    /// <summary>
    /// Link
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; } = "";
}