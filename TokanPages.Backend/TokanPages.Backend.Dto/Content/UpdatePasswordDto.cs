namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// UpdatePasswordDto
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdatePasswordDto : BaseClass
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
    /// LabelNewPassword
    /// </summary>
    [JsonProperty("labelNewPassword")]
    public string LabelNewPassword { get; set; } = "";

    /// <summary>
    /// LabelVerifyPassword
    /// </summary>
    [JsonProperty("labelVerifyPassword")]
    public string LabelVerifyPassword { get; set; } = "";
}