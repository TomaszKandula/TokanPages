namespace TokanPages.Backend.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Icon
/// </summary>
[ExcludeFromCodeCoverage]
public class Icon
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Link
    /// </summary>
    [JsonProperty("link")]
    public string Link { get; set; }
}