using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common;

namespace TokanPages.WebApi.Dto.Content;

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