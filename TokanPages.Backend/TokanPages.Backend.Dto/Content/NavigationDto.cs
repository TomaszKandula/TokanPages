namespace TokanPages.Backend.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class NavigationDto : BaseClass
{
    [JsonProperty("logo")]
    public string Logo { get; set; }

    [JsonProperty("menu")]
    public Menu Menu { get; set; }
}