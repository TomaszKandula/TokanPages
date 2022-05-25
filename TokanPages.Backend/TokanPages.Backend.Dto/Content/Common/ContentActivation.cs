namespace TokanPages.Backend.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ContentActivation
{
    [JsonProperty("type")]
    public string Type { get; set; }
        
    [JsonProperty("caption")]
    public string Caption { get; set; }
        
    [JsonProperty("text1")]
    public string Text1 { get; set; }
        
    [JsonProperty("text2")]
    public string Text2 { get; set; }
        
    [JsonProperty("button")]
    public string Button { get; set; }
}