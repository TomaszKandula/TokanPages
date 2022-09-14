using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

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