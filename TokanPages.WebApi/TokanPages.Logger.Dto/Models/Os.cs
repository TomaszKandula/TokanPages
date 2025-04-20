using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// Operating System details.
/// </summary>
[ExcludeFromCodeCoverage]
public class Os
{
    /// <summary>
    /// Operating System name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Operating System version.
    /// </summary>
    public string? Version { get; set; }
}
