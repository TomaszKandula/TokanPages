namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// UserSignoutDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UserSignoutDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; }

    /// <summary>
    /// OnProcessing
    /// </summary>
    [JsonProperty("onProcessing")]
    public string OnProcessing { get; set; }

    /// <summary>
    /// OnFinish
    /// </summary>
    [JsonProperty("onFinish")]
    public string OnFinish { get; set; }

    /// <summary>
    /// ButtonText
    /// </summary>
    [JsonProperty("buttonText")]
    public string ButtonText { get; set; }
}