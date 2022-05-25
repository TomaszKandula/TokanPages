namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSignupDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("consent")]
    public string Consent { get; set; }

    [JsonProperty("labelFirstName")]
    public string LabelFirstName { get; set; }

    [JsonProperty("labelLastName")]
    public string LabelLastName { get; set; }

    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }

    [JsonProperty("labelPassword")]
    public string LabelPassword { get; set; }
}