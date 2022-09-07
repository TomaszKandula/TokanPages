namespace TokanPages.WebApi.Dto.Content.Common;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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