namespace TokanPages.WebApi.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// ContactFormDto
/// </summary>
[ExcludeFromCodeCoverage]
public class ContactFormDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Text
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";

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
    /// LabelSubject
    /// </summary>
    [JsonProperty("labelSubject")]
    public string LabelSubject { get; set; } = "";

    /// <summary>
    /// LabelMessage
    /// </summary>
    [JsonProperty("labelMessage")]
    public string LabelMessage { get; set; } = "";
}