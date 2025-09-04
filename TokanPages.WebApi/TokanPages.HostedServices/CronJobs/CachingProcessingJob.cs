using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Base;
using TokanPages.HostedServices.CronJobs.Abstractions;
using TokanPages.HostedServices.Models;
using TokanPages.Services.SpaCachingService;

namespace TokanPages.HostedServices.CronJobs;

/// <summary>
/// CRON job implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingJob : CronJob
{
    private const string ServiceName = $"[{nameof(CachingProcessingJob)}]";

    private readonly ICachingService _cachingService;

    private readonly ILoggerService _loggerService;

    private readonly string _cronExpression;

    private readonly string _getActionUrl;

    private readonly string _postActionUrl;

    private readonly string[]? _filesToCache;
    
    private readonly List<RoutePath> _paths;

    /// <summary>
    /// CRON job implementation.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="cachingService"></param>
    /// <param name="loggerService"></param>
    public CachingProcessingJob(ICachingProcessingConfig config, 
        ICachingService cachingService, ILoggerService loggerService)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _cachingService = cachingService;
        _loggerService = loggerService;
        _cronExpression = config.CronExpression;
        _getActionUrl = config.GetActionUrl ?? "";
        _postActionUrl = config.PostActionUrl ?? "";
        _filesToCache = config.FilesToCache;
        _paths = config.RoutePaths;
    }

    /// <summary>
    /// Execute caching of the SPA.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: working...");
        await _cachingService.SaveStaticFiles(_filesToCache, _getActionUrl, _postActionUrl);

        if (_paths.Count == 0)
        {
            _loggerService.LogInformation($"{ServiceName}: no routes registered for caching..., quitting the job...");
            return;
        }

        foreach (var path in _paths)
        {
            var page = await _cachingService.RenderStaticPage(path.Url, _postActionUrl, path.Name);
            if (!string.IsNullOrWhiteSpace(page))
                _loggerService.LogInformation($"{ServiceName}: page '{path.Name}' has been rendered and saved. Url: '{path.Url}'.");
        }
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: started, CRON expression is '{_cronExpression}'.");

        var staticFileToCache = _filesToCache?.Length ?? 0;
        _loggerService.LogInformation($"{ServiceName}: {staticFileToCache} static file(s) to be cached.");
        if (_filesToCache is not null && _filesToCache.Length > 0)
        {
            foreach (var item in _filesToCache)
            {
                _loggerService.LogInformation($"{ServiceName}: ...to be cached: {item}");
            }
        }

        _loggerService.LogInformation($"{ServiceName}: {_paths.Count} SPA pages to be cached.");
        if (_paths.Count > 0)
        {
            foreach (var item in _paths)
            {
                _loggerService.LogInformation($"{ServiceName}: ...to be cached: {item.Name} (url: {item.Url})");
            }
        }

        Task.Run(async () => await _cachingService.GetBrowser(), cancellationToken);

        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{ServiceName}: stopped.");
        return base.StopAsync(cancellationToken);
    }
}