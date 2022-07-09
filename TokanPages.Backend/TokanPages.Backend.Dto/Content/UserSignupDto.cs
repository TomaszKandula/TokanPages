namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// UserSignupDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UserSignupDto : BaseClass
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
    /// Link
    /// </summary>
    [JsonProperty("link")]
    public string Link { get; set; } = "";

    /// <summary>
    /// Consent
    /// </summary>
    [JsonProperty("consent")]
    public string Consent { get; set; } = "";

    /// <summary>
    /// LabelFirstName
    /// </summary>
    [JsonProperty("labelFirstName")]
    public string LabelFirstName { get; set; } = "";

    /// <summary>
    /// LabelLastName
    /// </summary>
    [JsonProperty("labelLastName")]
    public string LabelLastName { get; set; } = "";

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