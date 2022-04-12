namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class FooterDto : BaseClass
{
    [JsonProperty("terms")]
    public string Terms { get; set; }

    [JsonProperty("policy")]
    public string Policy { get; set; }

    [JsonProperty("copyright")]
    public string Copyright { get; set; }

    [JsonProperty("reserved")]
    public string Reserved { get; set; }

    [JsonProperty("icons")]
    public List<Icon> Icons { get; set; }
}