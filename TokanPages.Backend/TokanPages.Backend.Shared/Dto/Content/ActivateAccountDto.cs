namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ActivateAccountDto : BaseClass
{
    [JsonProperty("onProcessing")]
    public ContentActivation OnProcessing { get; set; }
        
    [JsonProperty("onSuccess")]
    public ContentActivation OnSuccess { get; set; }
        
    [JsonProperty("onError")]
    public ContentActivation OnError { get; set; }
}