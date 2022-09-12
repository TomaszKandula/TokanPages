using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Base;

/// <summary>
/// Base class
/// </summary>
[ExcludeFromCodeCoverage]
public class BaseClass
{
    /// <summary>
    /// Language
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; } = "";
}