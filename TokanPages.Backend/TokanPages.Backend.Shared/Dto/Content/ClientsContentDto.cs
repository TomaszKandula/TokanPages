namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ClientsContentDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("images")]
    public List<string> Images { get; set; }
}