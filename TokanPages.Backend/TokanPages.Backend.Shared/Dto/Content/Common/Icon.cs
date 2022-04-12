namespace TokanPages.Backend.Shared.Dto.Content.Common;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Icon
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }
}