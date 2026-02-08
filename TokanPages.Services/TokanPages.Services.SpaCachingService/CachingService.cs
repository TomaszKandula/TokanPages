using System.Text;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;
using PuppeteerSharp.Media;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.Services.SpaCachingService.Abstractions;
using TokanPages.Services.SpaCachingService.Models;

namespace TokanPages.Services.SpaCachingService;

/// <summary>
/// Caching service implementation.
/// </summary>
internal sealed class CachingService : ICachingService
{
    private const int FiveMinutesTimeout = 300000;

    private const string DocumentFontReady = "document.fonts.ready";

    private const string ServiceName = $"[{nameof(CachingService)}]";

    private const string Script = @"
        () => {
            return {
                width: document.documentElement.scrollWidth,
                height: document.documentElement.scrollHeight
            };
        }
    ";

    private readonly ILoggerService _loggerService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly LaunchOptions _launchOptions = new()
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

    public CachingService(ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory, IHttpClientServiceFactory httpClientServiceFactory)
    {
        _loggerService = loggerService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _httpClientServiceFactory = httpClientServiceFactory;
    }

    /// <inheritdoc />
    public async Task GetBrowser()
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
            FatalError(exception);
        }
    }

    /// <inheritdoc />
    public async Task<string> GeneratePdf(string sourceUrl,  string? optionalName = default)
    {
        if (string.IsNullOrWhiteSpace(sourceUrl))
        {
            _loggerService.LogWarning($"{ServiceName}: No source URL found...");
            return string.Empty;
        }

        try
        {
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();

            await page.EmulateMediaTypeAsync(MediaType.Print);
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920, 
                Height = 1080
            });

            await page.GoToAsync(sourceUrl, FiveMinutesTimeout, waitUntil: WaitUntilOptions);
            await page.EvaluateExpressionHandleAsync(DocumentFontReady);

            // Set the viewport size based on the actual content size
            var contentSize = await page.EvaluateFunctionAsync<dynamic>(Script);
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = (int)contentSize.width,
                Height = (int)contentSize.height
            });            

            var pdfName = string.IsNullOrWhiteSpace(optionalName) ? $"{Guid.NewGuid()}.pdf" : $"{optionalName}.pdf";
            var tempDir = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.PathSeparator}temp";
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
                _loggerService.LogInformation($"{ServiceName}: Directory '{tempDir}' has been created.");
            }

            var outputPath = Path.Combine(tempDir, pdfName);
            var fileInfo = new FileInfo(outputPath);
            if (fileInfo.Exists)
                fileInfo.Delete();

            var pdfOptions = new PdfOptions
            {
                Format = PaperFormat.A4,
                Scale = (decimal)0.80,
                Landscape = false,
                DisplayHeaderFooter = false,
                PrintBackground = true,
                Width = (int)contentSize.width,
                Height = (int)contentSize.height
            };

            await using var stream = await page.PdfStreamAsync(pdfOptions);
            await using var file = new FileStream(outputPath, FileMode.Create);
            stream.Position = 0;
            await stream.CopyToAsync(file);
            await SaveToAzureStorage(outputPath, $"documents/{pdfName}");

            _loggerService.LogInformation($"{ServiceName}: PDF has been generated.");
            return outputPath;
        }
        catch (Exception exception)
        {
            FatalError(exception);
            return string.Empty;
        }
    }

    /// <inheritdoc />
    public async Task<string> RenderStaticPage(string sourceUrl, string serviceUrl, string pageName)
    {
        if (string.IsNullOrWhiteSpace(sourceUrl))
        {
            _loggerService.LogWarning($"{ServiceName}: No source URL found...");
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(serviceUrl))
        {
            _loggerService.LogWarning($"{ServiceName}: No service URL found...");
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(pageName))
        {
            _loggerService.LogWarning($"{ServiceName}: No page name found...");
            return string.Empty;
        }

        try
        {
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();

            await page.GoToAsync(sourceUrl, FiveMinutesTimeout, waitUntil: WaitUntilOptions);
            await page.EvaluateExpressionHandleAsync(DocumentFontReady);
            var htmlContent = await page.GetContentAsync();
            var binary = Encoding.UTF8.GetBytes(htmlContent);

            var fileName = $"{pageName}.html";
            await UploadFile(binary, fileName, serviceUrl);

            _loggerService.LogInformation($"{ServiceName}: Page has been rendered. Content length '{htmlContent.Length}'.");
            return fileName;
        }
        catch (Exception exception)
        {
            FatalError(exception);
            return string.Empty;
        }
    }

    /// <inheritdoc />
    public async Task<int> SaveStaticFiles(IEnumerable<string>? source, string sourceUrl, string serviceUrl)
    {
        if (source is null)
        {
            _loggerService.LogWarning($"{ServiceName}: No source files found...");
            return 0;
        }

        if (string.IsNullOrWhiteSpace(sourceUrl))
        {
            _loggerService.LogWarning($"{ServiceName}: No source URL found...");
            return 0;
        }

        if (string.IsNullOrWhiteSpace(serviceUrl))
        {
            _loggerService.LogWarning($"{ServiceName}: No service URL found...");
            return 0;
        }

        var processed = 0;
        var files = source as string[] ?? source.ToArray();
        _loggerService.LogInformation($"{ServiceName}: Saving {files.Length} file(s) to cache...");
        foreach (var fileName in files)
        {
            var url = $"{sourceUrl}/{fileName}"; // we may have fileName: static/js/bundle.js
            var fileContent = await GetFileFromUrl(url);
            _loggerService.LogInformation($"{ServiceName}: Received content from '{url}'...");

            if (fileContent is not null)
            {
                var name = Path.GetFileName(fileName); // only name w/extension here
                await UploadFile(fileContent, name, serviceUrl);
                processed += 1;
                _loggerService.LogInformation($"{ServiceName}: File '{fileName}' has been saved.");
            }
        }

        return processed;
    }

    private async Task<byte[]?> GetFileFromUrl(string url)
    {
        var configuration = new HttpClientSettings { Url = url, Method = "GET" };
        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute(configuration);
        return result.Content;
    }

    private async Task UploadFile(byte[] fileData, string fileName, string requestUrl)
    {
        var configuration = new HttpClientSettings 
        { 
            Url = requestUrl, 
            Method = "POST",
            FieldName = "BinaryData",
            FileName = fileName,
            FileData = fileData
        };

        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute<UploadFileOutputDto>(configuration);
        _loggerService.LogInformation($"{ServiceName}: File saved to local cache storage. Uploaded: {result.UploadedFileSize}. Left: {result.FreeSpace}.");
    }

    private async Task SaveToAzureStorage(string sourcePath, string destination)
    {
        var azureBlob = _azureBlobStorageFactory.Create(_loggerService);
        var fileToUpload = await File.ReadAllBytesAsync(sourcePath);
        using var fileStream = new MemoryStream(fileToUpload);
        await azureBlob.UploadFile(fileStream, $"content/assets/{destination}", "text/html");
    }

    private void FatalError(Exception exception)
    {
        _loggerService.LogError($"{ServiceName}: Critical error occured but will try to execute next time. Details follows...");
        _loggerService.LogError($"{ServiceName}: Error message: {exception.Message}. Details: {exception.InnerException?.Message ?? "n/a"}.");
    }
}