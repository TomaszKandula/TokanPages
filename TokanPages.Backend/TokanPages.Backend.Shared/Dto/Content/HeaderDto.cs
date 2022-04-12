namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class HeaderDto : BaseClass
{
    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("action")]
    public string Action { get; set; }
}