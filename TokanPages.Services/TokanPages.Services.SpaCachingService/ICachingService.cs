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
    /// <returns>Returns an async. task.</returns>
    Task RenderStaticPage(string sourceUrl, string pageName);
}