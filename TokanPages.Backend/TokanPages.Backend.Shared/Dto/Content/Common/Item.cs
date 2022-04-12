#nullable enable
namespace TokanPages.Backend.Shared.Dto.Content.Common;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Item : Subitem
{
    [JsonProperty("subitems")]
    public List<Subitem>? Subitems { get; set; }
}