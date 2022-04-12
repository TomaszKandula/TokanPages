namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class NewsletterDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }
}