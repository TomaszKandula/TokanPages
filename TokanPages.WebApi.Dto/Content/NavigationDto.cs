namespace TokanPages.WebApi.Dto.Content;

using Base;
using Common;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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