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
    /// <param name="sourceUrl">Web page URL - to be rendered.</param>
    /// <param name="serviceUrl">Upload service URL (POST action).</param>
    /// <param name="pageName">Rendered HTML page name.</param>
    /// <returns>Returns full path to a generated HTML.</returns>
    Task<string> RenderStaticPage(string sourceUrl, string serviceUrl, string pageName);

    /// <summary>
    /// Save to cache folder all the listed files.
    /// </summary>
    /// <param name="source">File list to be cached.</param>
    /// <param name="sourceUrl">Source URL - file source.</param>
    /// <param name="serviceUrl">Upload service URL (POST action).</param>
    /// <returns>Number of processed files.</returns>
    Task<int> SaveStaticFiles(IEnumerable<string>? source, string sourceUrl, string serviceUrl);
}