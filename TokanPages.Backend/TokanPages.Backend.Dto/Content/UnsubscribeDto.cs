namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// UnsubscribeDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UnsubscribeDto : BaseClass
{
    /// <summary>
    /// ContentPre
    /// </summary>
    [JsonProperty("contentPre")]
    public ContentUnsubscribe ContentPre { get; set; } = new();

    /// <summary>
    /// ContentPost
    /// </summary>
    [JsonProperty("contentPost")]
    public ContentUnsubscribe ContentPost { get; set; } = new();
}