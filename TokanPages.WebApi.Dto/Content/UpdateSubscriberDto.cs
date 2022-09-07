namespace TokanPages.WebApi.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// UpdateSubscriberDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateSubscriberDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";

    /// <summary>
    /// LabelEmail
    /// </summary>
    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; } = "";
}