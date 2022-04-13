namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class WrongPagePromptDto : BaseClass
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("header")]
    public string Header { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }
}