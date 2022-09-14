using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using TokanPages.WebApi.Dto.Content.Base;
using TokanPages.WebApi.Dto.Content.Common.AccountSections;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// AccountDto
/// </summary>
[ExcludeFromCodeCoverage]
public class AccountDto : BaseClass
{
    /// <summary>
    /// SectionAccessDenied
    /// </summary>
    [JsonProperty("sectionAccessDenied")]
    public SectionAccessDenied SectionAccessDenied { get; set; } = new();

    /// <summary>
    /// SectionAccountInformation
    /// </summary>
    [JsonProperty("sectionAccountInformation")]
    public SectionAccountInformation SectionAccountInformation { get; set; } = new();

    /// <summary>
    /// SectionAccountPassword
    /// </summary>
    [JsonProperty("sectionAccountPassword")]
    public SectionAccountPassword SectionAccountPassword { get; set; } = new();

    /// <summary>
    /// SectionAccountRemoval
    /// </summary>
    [JsonProperty("sectionAccountRemoval")]
    public SectionAccountRemoval SectionAccountRemoval { get; set; } = new();
}