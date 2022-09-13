using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// ActivateAccountDto
/// </summary>
[ExcludeFromCodeCoverage]
public class ActivateAccountDto : BaseClass
{
    /// <summary>
    /// OnProcessing
    /// </summary>
    [JsonProperty("onProcessing")]
    public ContentActivation OnProcessing { get; set; } = new();
        
    /// <summary>
    /// OnSuccess
    /// </summary>
    [JsonProperty("onSuccess")]
    public ContentActivation OnSuccess { get; set; } = new();
        
    /// <summary>
    /// OnError
    /// </summary>
    [JsonProperty("onError")]
    public ContentActivation OnError { get; set; } = new();
}