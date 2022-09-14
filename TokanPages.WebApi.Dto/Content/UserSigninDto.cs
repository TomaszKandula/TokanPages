using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// UserSigninDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UserSigninDto : BaseClass
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
    /// Link1
    /// </summary>
    [JsonProperty("link1")]
    public string Link1 { get; set; } = "";

    /// <summary>
    /// Link2
    /// </summary>
    [JsonProperty("link2")]
    public string Link2 { get; set; } = "";

    /// <summary>
    /// LabelEmail
    /// </summary>
    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; } = "";

    /// <summary>
    /// LabelPassword
    /// </summary>
    [JsonProperty("labelPassword")]
    public string LabelPassword { get; set; } = "";
}