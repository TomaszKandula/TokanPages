namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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
    public string Caption { get; set; }

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; }

    /// <summary>
    /// Link1
    /// </summary>
    [JsonProperty("link1")]
    public string Link1 { get; set; }

    /// <summary>
    /// Link2
    /// </summary>
    [JsonProperty("link2")]
    public string Link2 { get; set; }

    /// <summary>
    /// LabelEmail
    /// </summary>
    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }

    /// <summary>
    /// LabelPassword
    /// </summary>
    [JsonProperty("labelPassword")]
    public string LabelPassword { get; set; }
}