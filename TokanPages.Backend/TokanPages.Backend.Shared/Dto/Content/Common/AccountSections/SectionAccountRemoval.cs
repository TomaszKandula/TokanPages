namespace TokanPages.Backend.Shared.Dto.Content.Common.AccountSections;

using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SectionAccountRemoval
{
    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("warningText")]
    public string WarningText { get; set; }

    [JsonProperty("deleteButtonText")]
    public string DeleteButtonText { get; set; }
}