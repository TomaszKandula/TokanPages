namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
    public string Caption { get; set; }

    /// <summary>
    /// Images
    /// </summary>
    [JsonProperty("images")]
    public List<string> Images { get; set; }
}