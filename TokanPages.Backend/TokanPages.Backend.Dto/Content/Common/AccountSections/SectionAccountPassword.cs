namespace TokanPages.Backend.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// SectionAccountPassword
/// </summary>
[ExcludeFromCodeCoverage]
public class SectionAccountPassword
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// LabelOldPassword
    /// </summary>
    [JsonProperty("labelOldPassword")]
    public string LabelOldPassword { get; set; } = "";

    /// <summary>
    /// LabelNewPassword
    /// </summary>
    [JsonProperty("labelNewPassword")]
    public string LabelNewPassword { get; set; } = "";

    /// <summary>
    /// LabelConfirmPassword
    /// </summary>
    [JsonProperty("labelConfirmPassword")]
    public string LabelConfirmPassword { get; set; } = "";

    /// <summary>
    /// UpdateButtonText
    /// </summary>
    [JsonProperty("updateButtonText")]
    public string UpdateButtonText { get; set; } = "";
}