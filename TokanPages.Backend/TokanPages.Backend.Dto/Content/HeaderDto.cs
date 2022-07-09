namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// HeaderDto
/// </summary>
[ExcludeFromCodeCoverage]
public class HeaderDto : BaseClass
{
    /// <summary>
    /// Photo
    /// </summary>
    [JsonProperty("photo")]
    public string Photo { get; set; } = "";

    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = "";

    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public Link Action { get; set; } = new();
}