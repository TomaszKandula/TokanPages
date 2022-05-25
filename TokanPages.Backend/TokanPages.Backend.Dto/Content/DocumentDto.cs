namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DocumentDto : BaseClass
{
    [JsonProperty("items")]
    public List<Section> Items { get; set; }
}