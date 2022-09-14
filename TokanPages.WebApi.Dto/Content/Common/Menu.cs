using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.WebApi.Dto.Content.Common;

/// <summary>
/// Menu
/// </summary>
[ExcludeFromCodeCoverage]
public class Menu
{
    /// <summary>
    /// Image
    /// </summary>
    [JsonProperty("image")]
    public string Image { get; set;  } = "";

    /// <summary>
    /// Items
    /// </summary>
    [JsonProperty("items")]
    public List<Item> Items { get; set; } = new();
}