namespace TokanPages.WebApi.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// NotFoundDto
/// </summary>
[ExcludeFromCodeCoverage]
public class NotFoundDto : BaseClass
{
    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; } = "";

    /// <summary>
    /// Header
    /// </summary>
    [JsonProperty("header")]
    public string Header { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";
}