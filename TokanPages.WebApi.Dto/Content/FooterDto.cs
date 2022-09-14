using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// FooterDto
/// </summary>
[ExcludeFromCodeCoverage]
public class FooterDto : BaseClass
{
    /// <summary>
    /// Terms
    /// </summary>
    [JsonProperty("terms")]
    public Link Terms { get; set; } = new();

    /// <summary>
    /// Policy
    /// </summary>
    [JsonProperty("policy")]
    public Link Policy { get; set; } = new();

    /// <summary>
    /// Copyright
    /// </summary>
    [JsonProperty("copyright")]
    public string Copyright { get; set; } = "";

    /// <summary>
    /// Reserved
    /// </summary>
    [JsonProperty("reserved")]
    public string Reserved { get; set; } = "";

    /// <summary>
    /// Icons
    /// </summary>
    [JsonProperty("icons")]
    public List<Icon> Icons { get; set; } = new();
}