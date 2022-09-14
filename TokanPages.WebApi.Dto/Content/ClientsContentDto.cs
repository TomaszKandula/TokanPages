using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// ClientsContentDto
/// </summary>
[ExcludeFromCodeCoverage]
public class ClientsContentDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Images
    /// </summary>
    [JsonProperty("images")]
    public List<string> Images { get; set; } = new();
}