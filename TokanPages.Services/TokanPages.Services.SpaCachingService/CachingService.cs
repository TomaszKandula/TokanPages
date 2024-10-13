using PuppeteerSharp;
using PuppeteerSharp.BrowserData;

namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service implementation.
/// </summary>
public class CachingService : ICachingService
{
    private const string PdfDirName = "PdfDir";

    private const string CacheDirName = "CacheDir";

    private const string DocumentFontReady = "document.fonts.ready";

    private static LaunchOptions _launchOptions = new()
    {
        Headless = true,
        HeadlessMode = HeadlessMode.True,
        LogProcess = true,
        Args = new []
        {
            "--disable-gpu",
            "--disable-dev-shm-usage",
            "--disable-setuid-sandbox",
            "--no-sandbox"
        }
    };

    private static readonly WaitUntilNavigation[] WaitUntilOptions = {
        WaitUntilNavigation.Load,
        WaitUntilNavigation.DOMContentLoaded,
        WaitUntilNavigation.Networkidle0
    };

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
        await GetBrowser();
        await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
        await using var page = await browser.NewPageAsync();

        await page.GoToAsync(sourceUrl, waitUntil: WaitUntilOptions);
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
        await GetBrowser();
        await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
        await using var page = await browser.NewPageAsync();

        await page.GoToAsync(sourceUrl, waitUntil: WaitUntilOptions);
        await page.EvaluateExpressionHandleAsync(DocumentFontReady);
        var htmlContent = await page.GetContentAsync();

        var outputPath = Path.Combine(CacheDir, $"{pageName}.html");
        await File.WriteAllTextAsync(outputPath, htmlContent);
        return outputPath;
    }

    private static async Task GetBrowser()
    {
        var browserFetcher = new BrowserFetcher
        {
            Browser = SupportedBrowser.Chrome
        };

        await browserFetcher.DownloadAsync();
        var path = browserFetcher
            .GetInstalledBrowsers()
            .First(browser => browser.BuildId == Chrome.DefaultBuildId)
            .GetExecutablePath();

        _launchOptions.ExecutablePath = path;
    }
}