namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UnsubscribeDto : BaseClass
{
    [JsonProperty("contentPre")]
    public ContentUnsubscribe ContentPre { get; set; }

    [JsonProperty("contentPost")]
    public ContentUnsubscribe ContentPost { get; set; }
}