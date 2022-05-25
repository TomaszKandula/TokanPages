namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserSignoutDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("onProcessing")]
    public string OnProcessing { get; set; }

    [JsonProperty("onFinish")]
    public string OnFinish { get; set; }

    [JsonProperty("buttonText")]
    public string ButtonText { get; set; }
}