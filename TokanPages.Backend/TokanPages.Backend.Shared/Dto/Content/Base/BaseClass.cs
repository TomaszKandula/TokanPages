namespace TokanPages.Backend.Shared.Dto.Content.Base;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class BaseClass
{
    [JsonProperty("language")]
    public string Language { get; set; }
}