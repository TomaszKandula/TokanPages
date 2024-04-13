namespace TokanPages.Gateway.Models;

/// <summary>
/// Response message headers.
/// </summary>
public class ResponseMessageHeaders
{
    /// <summary>
    /// Excluded headers.
    /// </summary>
    public IEnumerable<string>? Exclude { get; set; }
}