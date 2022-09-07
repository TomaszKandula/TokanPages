namespace TokanPages.WebApi.Dto.Content.Base;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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