namespace TokanPages.Backend.Shared.Dto.Content.Common;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Menu
{
    [JsonProperty("image")]
    public string Image { get; set;  }

    [JsonProperty("items")]
    public List<Item> Items { get; set; }
}