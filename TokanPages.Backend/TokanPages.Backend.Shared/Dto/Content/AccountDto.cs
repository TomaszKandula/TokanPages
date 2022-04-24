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

    [JsonProperty("sectionBasicInformation")]
    public SectionBasicInformation SectionBasicInformation { get; set; }
}