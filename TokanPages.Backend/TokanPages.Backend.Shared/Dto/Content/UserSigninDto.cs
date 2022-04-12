namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSigninDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("link1")]
    public string Link1 { get; set; }

    [JsonProperty("link2")]
    public string Link2 { get; set; }

    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }

    [JsonProperty("labelPassword")]
    public string LabelPassword { get; set; }
}