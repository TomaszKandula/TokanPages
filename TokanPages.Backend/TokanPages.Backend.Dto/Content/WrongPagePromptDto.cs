namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// WrongPagePromptDto
/// </summary>
[ExcludeFromCodeCoverage]
public class WrongPagePromptDto : BaseClass
{
    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Header
    /// </summary>
    [JsonProperty("header")]
    public string Header { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; }
}