using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common;

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