namespace TokanPages.Backend.Shared.Dto.Content;

using Base;
using Common.AccountSections;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AccountDto : BaseClass
{
    [JsonProperty("sectionAccessDenied")]
    public SectionAccessDenied SectionAccessDenied { get; set; }

    [JsonProperty("sectionAccountInformation")]
    public SectionAccountInformation SectionAccountInformation { get; set; }

    [JsonProperty("sectionAccountPassword")]
    public SectionAccountPassword SectionAccountPassword { get; set; }

    [JsonProperty("sectionAccountRemoval")]
    public SectionAccountRemoval SectionAccountRemoval { get; set; }
}