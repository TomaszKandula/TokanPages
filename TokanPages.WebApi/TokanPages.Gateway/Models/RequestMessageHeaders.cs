namespace TokanPages.Gateway.Models;

/// <summary>
/// Request message headers.
/// </summary>
public class RequestMessageHeaders
{
    /// <summary>
    /// Excluded headers.
    /// </summary>
    public IEnumerable<string>? Exclude { get; set; }
}