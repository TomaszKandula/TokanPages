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
    /// <param name="waitForRender">Optional wait parameter.</param>
    /// <returns>Returns full path to a generated HTML.</returns>
    Task<string> RenderStaticPage(string sourceUrl, string pageName, int waitForRender = 500);
}