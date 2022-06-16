namespace TokanPages.Backend.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// SectionAccountInformation
/// </summary>
[ExcludeFromCodeCoverage]
public class SectionAccountInformation
{
    /// <summary>
    /// Caption
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = "";

    /// <summary>
    /// LabelUserId
    /// </summary>
    [JsonProperty("labelUserId")]
    public string LabelUserId { get; set; } = "";

    /// <summary>
    /// LabelUserAlias
    /// </summary>
    [JsonProperty("labelUserAlias")]
    public string LabelUserAlias { get; set; } = ""; 

    /// <summary>
    /// LabelFirstName
    /// </summary>
    [JsonProperty("labelFirstName")]
    public string LabelFirstName { get; set; } = "";

    /// <summary>
    /// LabelLastName
    /// </summary>
    [JsonProperty("labelLastName")]
    public string LabelLastName { get; set; } = "";

    /// <summary>
    /// LabelEmail
    /// </summary>
    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; } = "";

    /// <summary>
    /// LabelShortBio
    /// </summary>
    [JsonProperty("labelShortBio")]
    public string LabelShortBio { get; set; } = "";

    /// <summary>
    /// LabelUserAvatar
    /// </summary>
    [JsonProperty("labelUserAvatar")]
    public string LabelUserAvatar { get; set; } = "";

    /// <summary>
    /// LabelIsActivated
    /// </summary>
    [JsonProperty("labelIsActivated")]
    public string LabelIsActivated { get; set; } = "";

    /// <summary>
    /// IsActivatedText
    /// </summary>
    [JsonProperty("isActivatedText")]
    public string IsActivatedText { get; set; } = "";

    /// <summary>
    /// UpdateButtonText
    /// </summary>
    [JsonProperty("updateButtonText")]
    public string UpdateButtonText { get; set; } = "";

    /// <summary>
    /// UploadAvatarButtonText
    /// </summary>
    [JsonProperty("uploadAvatarButtonText")]
    public string UploadAvatarButtonText { get; set; } = "";
}