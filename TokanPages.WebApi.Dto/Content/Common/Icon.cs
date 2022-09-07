namespace TokanPages.WebApi.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Icon
/// </summary>
[ExcludeFromCodeCoverage]
public class Icon
{
    /// <summary>
    /// Icon name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Link
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; } = "";
}