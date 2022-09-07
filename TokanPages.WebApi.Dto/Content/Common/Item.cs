namespace TokanPages.WebApi.Dto.Content.Common;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Item
/// </summary>
[ExcludeFromCodeCoverage]
public class Item : Subitem
{
    /// <summary>
    /// Subitems
    /// </summary>
    [JsonProperty("subitems")]
    public List<Subitem>? Subitems { get; set; }
}