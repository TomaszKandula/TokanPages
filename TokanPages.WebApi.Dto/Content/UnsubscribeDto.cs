using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

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