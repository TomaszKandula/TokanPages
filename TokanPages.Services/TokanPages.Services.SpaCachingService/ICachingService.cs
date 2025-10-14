namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service contract.
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Downloads OS specific browser for page rendering.
    /// </summary>
    /// <returns></returns>
    Task GetBrowser();

    /// <summary>
    /// Generates PDF file of given web page URL.
    /// </summary>
    /// <remarks>
    /// It requires browser to be already downloaded.
    /// </remarks>
    /// <param name="sourceUrl">Web page URL.</param>
    /// <param name="optionalName">Optional PDF file name. Otherwise, name will be generated.</param>
    /// <returns>Returns full path to a PDF file.</returns>
    Task<string> GeneratePdf(string sourceUrl, string? optionalName);

    /// <summary>
    /// Renders static page.
    /// </summary>
    /// <remarks>
    /// It requires browser to be already downloaded.
    /// </remarks>
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