namespace TokanPages.Backend.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SectionAccessDenied
{
    [JsonProperty("accessDeniedCaption")]
    public string AccessDeniedCaption { get; set; }

    [JsonProperty("accessDeniedPrompt")]
    public string AccessDeniedPrompt { get; set; }

    [JsonProperty("homeButtonText")]
    public string HomeButtonText { get; set; }
}