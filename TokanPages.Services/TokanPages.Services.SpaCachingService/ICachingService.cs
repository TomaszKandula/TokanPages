namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service contract.
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Generates PDF file of given web page URL.
    /// </summary>
    /// <param name="sourceUrl">Web page URL.</param>
    /// <returns>Returns full path to a PDF file.</returns>
    Task<string> GeneratePdf(string sourceUrl);

    /// <summary>
    /// Renders static page.
    /// </summary>
    /// <param name="sourceUrl">Web page URL.</param>
    /// <param name="pageName">Page name.</param>
    /// <returns>Returns full path to a generated HTML.</returns>
    Task<string> RenderStaticPage(string sourceUrl, string pageName);

    /// <summary>
    /// Save to cache folder all the listed files.
    /// </summary>
    /// <param name="source">File list to be cached.</param>
    /// <param name="baseUrl">Base URL of a file host.</param>
    /// <returns>Number of processed files.</returns>
    Task<int> SaveStaticFiles(IEnumerable<string> source, string baseUrl);
}