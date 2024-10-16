using PuppeteerSharp;
using PuppeteerSharp.BrowserData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service implementation.
/// </summary>
public class CachingService : ICachingService
{
    private const string PdfDirName = "PdfDir";

    private const string CacheDirName = "CacheDir";

    private const string DocumentFontReady = "document.fonts.ready";

    private const string ServiceName = $"[{nameof(CachingService)}]";

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly ILoggerService _loggerService;

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

    private string PdfDir { get; }

    private string CacheDir { get; }

    public CachingService(ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory)
    {
        _loggerService = loggerService;
        _azureBlobStorageFactory = azureBlobStorageFactory;

        PdfDir = GetPdfDir();
        CacheDir = GetCacheDir();
        EnsureWorkingDirectories();
    }

    /// <inheritdoc />
    public async Task<string> GeneratePdf(string sourceUrl)
    {
        await GetBrowser();

        try
        {
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();

            await page.GoToAsync(sourceUrl, waitUntil: WaitUntilOptions);
            await page.EvaluateExpressionHandleAsync(DocumentFontReady);

            var pdfName = $"{Guid.NewGuid()}.pdf";
            var outputPath = Path.Combine(PdfDir, pdfName);
            var fileInfo = new FileInfo(outputPath);
            if (fileInfo.Exists)
                fileInfo.Delete();

            await page.PdfAsync(outputPath);
            await SaveToAzureStorage(outputPath, $"documents/{pdfName}");

            _loggerService.LogInformation($"{ServiceName}: PDF has been generated.");
            return outputPath;
        }
        catch (Exception exception)
        {
            throw FatalError(exception);
        }
    }

    /// <inheritdoc />
    public async Task<string> RenderStaticPage(string sourceUrl, string pageName)
    {
        await GetBrowser();

        try
        {
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();

            await page.GoToAsync(sourceUrl, waitUntil: WaitUntilOptions);
            await page.EvaluateExpressionHandleAsync(DocumentFontReady);
            var htmlContent = await page.GetContentAsync();

            var fileName = $"{pageName}.html";
            var outputPath = Path.Combine(CacheDir, fileName);
            await File.WriteAllTextAsync(outputPath, htmlContent);
            await SaveToAzureStorage(outputPath, $"cache/{fileName}");

            _loggerService.LogInformation($"{ServiceName}: Page has been rendered. Content length '{htmlContent.Length}'.");
            return outputPath;
        }
        catch (Exception exception)
        {
            throw FatalError(exception);
        }
    }

    private async Task SaveToAzureStorage(string sourcePath, string destination)
    {
        var azureBlob = _azureBlobStorageFactory.Create(_loggerService);
        var fileToUpload = await File.ReadAllBytesAsync(sourcePath);
        using var fileStream = new MemoryStream(fileToUpload);
        await azureBlob.UploadFile(fileStream, $"content/assets/{destination}", "text/html");
    }

    private static string GetPdfDir()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(baseDirectory, PdfDirName);
    }

    private static string GetCacheDir()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(baseDirectory, CacheDirName);
    }

    private void EnsureWorkingDirectories()
    {
        if (!Directory.Exists(PdfDir))
        {
            Directory.CreateDirectory(PdfDir);
            _loggerService.LogInformation($"{ServiceName}: Directory '{PdfDir}' has been created.");
        }

        if (!Directory.Exists(CacheDir))
        {
            Directory.CreateDirectory(CacheDir);
            _loggerService.LogInformation($"{ServiceName}: Directory '{CacheDir}' has been created.");
        }
    }

    private async Task GetBrowser()
    {
        try
        {
            _loggerService.LogInformation($"{ServiceName}: Getting Chrome browser (ver. {Chrome.DefaultBuildId})...");

            var browserFetcher = new BrowserFetcher { Browser = SupportedBrowser.Chrome };
            await browserFetcher.DownloadAsync();
            var path = browserFetcher
                .GetInstalledBrowsers()
                .First(browser => browser.BuildId == Chrome.DefaultBuildId)
                .GetExecutablePath();

            _launchOptions.ExecutablePath = path;
            _loggerService.LogInformation($"{ServiceName}: Browser downloaded, path '{path}'.");
        }
        catch (Exception exception)
        {
            throw FatalError(exception);
        }
    }

    private Exception FatalError(Exception exception)
    {
        _loggerService.LogError($"{ServiceName}: Error message: {exception.Message}. Details: {exception.InnerException?.Message ?? "n/a"}.");
        return new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
    }
}