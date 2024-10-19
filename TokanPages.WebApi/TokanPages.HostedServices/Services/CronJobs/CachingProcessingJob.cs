using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services.Abstractions;
using TokanPages.HostedServices.Services.Base;
using TokanPages.HostedServices.Services.Models;
using TokanPages.Services.SpaCachingService;

namespace TokanPages.HostedServices.Services.CronJobs;

/// <summary>
/// CRON job implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingJob : CronJob
{
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
        _getActionUrl = config.GetActionUrl;
        _postActionUrl = config.PostActionUrl;
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
        _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: working...");
        if (_paths.Count == 0)
        {
            _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: no routes registered for caching..., quitting the job...");
            return;
        }

        await _cachingService.SaveStaticFiles(_filesToCache, _getActionUrl, _postActionUrl);
        foreach (var path in _paths)
        {
            await _cachingService.RenderStaticPage(path.Url, _postActionUrl, path.Name);
            _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: page '{path.Name}' has been rendered and saved. Url: '{path.Url}'.");
        }
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: started, CRON expression is '{_cronExpression}'.");
        _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: routes for caching: {_paths.Count}.");
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"[{nameof(CachingProcessingJob)}]: stopped.");
        return base.StopAsync(cancellationToken);
    }
}