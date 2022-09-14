using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// NavigationDto
/// </summary>
[ExcludeFromCodeCoverage]
public class NavigationDto : BaseClass
{
    /// <summary>
    /// Logo
    /// </summary>
    [JsonProperty("logo")]
    public string Logo { get; set; } = "";

    /// <summary>
    /// Menu
    /// </summary>
    [JsonProperty("menu")]
    public Menu Menu { get; set; } = new();
}