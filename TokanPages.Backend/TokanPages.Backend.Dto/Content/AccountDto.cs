namespace TokanPages.Backend.Dto.Content;

using Base;
using Common.AccountSections;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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
    public SectionAccessDenied SectionAccessDenied { get; set; }

    /// <summary>
    /// SectionAccountInformation
    /// </summary>
    [JsonProperty("sectionAccountInformation")]
    public SectionAccountInformation SectionAccountInformation { get; set; }

    /// <summary>
    /// SectionAccountPassword
    /// </summary>
    [JsonProperty("sectionAccountPassword")]
    public SectionAccountPassword SectionAccountPassword { get; set; }

    /// <summary>
    /// SectionAccountRemoval
    /// </summary>
    [JsonProperty("sectionAccountRemoval")]
    public SectionAccountRemoval SectionAccountRemoval { get; set; }
}