#nullable enable
namespace TokanPages.Backend.Shared.Dto.Content.Common;

using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Subitem
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("icon")]
    public string? Icon { get; set; }

    [JsonProperty("enabled")]
    public bool? Enabled { get; set; }
}