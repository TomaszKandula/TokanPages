using PuppeteerSharp;

namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service implementation.
/// </summary>
public class CachingService : ICachingService
{
    private const string PdfDirName = "PdfDir";

    private const string CacheDirName = "CacheDir";

    private const string DocumentFontReady = "document.fonts.ready";

    public string PdfDir { get; }

    public string CacheDir { get; }

    public CachingService()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var pdfDir = Path.Combine(baseDirectory, PdfDirName);
        var cacheDir = Path.Combine(baseDirectory, CacheDirName);

        PdfDir = pdfDir;
        CacheDir = cacheDir;

        if (!Directory.Exists(PdfDir))
            Directory.CreateDirectory(PdfDir);

        if (!Directory.Exists(CacheDir))
            Directory.CreateDirectory(CacheDir);
    }

    /// <inheritdoc />
    public async Task<string> GeneratePdf(string sourceUrl)
    {
        var browserFetcher = new BrowserFetcher();
        var launchOptions = new LaunchOptions
        {
            Headless = true
        };

        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(launchOptions);
        await using var page = await browser.NewPageAsync();

        await page.GoToAsync(sourceUrl);
        await page.EvaluateExpressionHandleAsync(DocumentFontReady);

        var outputPath = Path.Combine(PdfDir, $"{Guid.NewGuid()}.pdf");
        var fileInfo = new FileInfo(outputPath);
        if (fileInfo.Exists)
            fileInfo.Delete();

        await page.PdfAsync(outputPath);
        return outputPath;
    }

    /// <inheritdoc />
    public async Task<string> RenderStaticPage(string sourceUrl, string pageName)
    {
        var browserFetcher = new BrowserFetcher();
        var launchOptions = new LaunchOptions
        {
            Headless = true
        };

        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(launchOptions);
        await using var page = await browser.NewPageAsync();

        await page.GoToAsync(sourceUrl);
        await page.EvaluateExpressionHandleAsync(DocumentFontReady);
        var htmlContent = await page.GetContentAsync();

        var outputPath = Path.Combine(CacheDir, $"{pageName}.html");
        await File.WriteAllTextAsync(outputPath, htmlContent);
        return outputPath;
    }
}