namespace TokanPages.Backend.Dto.Content;

using Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// FooterDto
/// </summary>
[ExcludeFromCodeCoverage]
public class GetContentManifestDto
{
    /// <summary>
    /// Manifest version
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; } = "";
    
    /// <summary>
    /// Create date and time
    /// </summary>
    [JsonProperty("created")]
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Update date and time
    /// </summary>
    [JsonProperty("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    /// Registered languages
    /// </summary>
    [JsonProperty("languages")]
    public List<LanguageItem> Languages { get; set; } = new();
}