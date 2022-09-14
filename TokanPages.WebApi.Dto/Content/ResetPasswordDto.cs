using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// ResetPasswordDto
/// </summary>
[ExcludeFromCodeCoverage]
public class ResetPasswordDto : BaseClass
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// Button
    /// </summary>
    [JsonProperty("button")]
    public string Button { get; set; } = "";

    /// <summary>
    /// LabelEmail
    /// </summary>
    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; } = "";
}