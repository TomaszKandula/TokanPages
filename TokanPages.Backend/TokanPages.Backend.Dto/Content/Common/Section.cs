namespace TokanPages.Backend.Dto.Content.Common;

using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Section
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
        
    [JsonProperty("value")]
    public dynamic Value { get; set; }
        
    [JsonProperty("prop")]
    public string Prop { get; set; }
        
    [JsonProperty("text")]
    public string Text { get; set; }
}