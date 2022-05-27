namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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
    public ContentActivation OnProcessing { get; set; }
        
    /// <summary>
    /// OnSuccess
    /// </summary>
    [JsonProperty("onSuccess")]
    public ContentActivation OnSuccess { get; set; }
        
    /// <summary>
    /// OnError
    /// </summary>
    [JsonProperty("onError")]
    public ContentActivation OnError { get; set; }
}