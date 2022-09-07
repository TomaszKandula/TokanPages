namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// DocumentDto
/// </summary>
[ExcludeFromCodeCoverage]
public class DocumentDto : BaseClass
{
    /// <summary>
    /// Items
    /// </summary>
    [JsonProperty("items")]
    public List<Section> Items { get; set; } = new();
}