namespace TokanPages.Backend.Dto.Content;

using Base;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdatePasswordDto : BaseClass
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("button")]
    public string Button { get; set; }

    [JsonProperty("labelNewPassword")]
    public string LabelNewPassword { get; set; }

    [JsonProperty("labelVerifyPassword")]
    public string LabelVerifyPassword { get; set; }
}