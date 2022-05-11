namespace TokanPages.Backend.Shared.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SectionBasicInformation
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("labelUserId")]
    public string LabelUserId { get; set; }

    [JsonProperty("labelUserAlias")]
    public string LabelUserAlias { get; set; }    

    [JsonProperty("labelFirstName")]
    public string LabelFirstName { get; set; }

    [JsonProperty("labelLastName")]
    public string LabelLastName { get; set; }

    [JsonProperty("labelEmail")]
    public string LabelEmail { get; set; }

    [JsonProperty("labelShortBio")]
    public string LabelShortBio { get; set; }

    [JsonProperty("labelUserAvatar")]
    public string LabelUserAvatar { get; set; }

    [JsonProperty("updateButtonText")]
    public string UpdateButtonText { get; set; }

    [JsonProperty("uploadAvatarButtonText")]
    public string UploadAvatarButtonText { get; set; }
}