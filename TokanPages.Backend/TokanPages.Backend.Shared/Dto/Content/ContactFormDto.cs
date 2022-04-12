namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ContactFormDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("consent")]
    public string Consent { get; set; }

    [JsonProperty("labelFirstName")]
    public string LabelFirstName { get; set; }

    [JsonProperty("labelLastName")]
    public string LabelLastName { get; set; }

    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }

    [JsonProperty("labelSubject")]
    public string LabelSubject { get; set; }

    [JsonProperty("labelMessage")]
    public string LabelMessage { get; set; }
}