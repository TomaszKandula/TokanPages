namespace TokanPages.Backend.Shared.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SectionAccountPassword
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("labelOldPassword")]
    public string LabelOldPassword { get; set; }

    [JsonProperty("labelNewPassword")]
    public string LabelNewPassword { get; set; }

    [JsonProperty("labelConfirmPassword")]
    public string LabelConfirmPassword { get; set; }

    [JsonProperty("updateButtonText")]
    public string UpdateButtonText { get; set; }
}