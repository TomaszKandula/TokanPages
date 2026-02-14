using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.HostedServices.Base;
using TokanPages.HostedServices.CronJobs.Abstractions;
using TokanPages.HostedServices.Models;
using TokanPages.Services.SpaCachingService.Abstractions;

namespace TokanPages.HostedServices.CronJobs;

/// <summary>
/// CRON job implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingJob : CronJob
{
    private const string ServiceName = $"[{nameof(CachingProcessingJob)}]";

    private readonly string _cronExpression;

    private readonly string _getActionUrl;

    private readonly string _postActionUrl;

    private readonly string[]? _filesToCache;
    
    private readonly List<RoutePath> _pagePaths;

    private readonly List<RoutePath> _pdfPaths;

    /// <summary>
    /// CRON job implementation.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="loggerService"></param>
    /// <param name="serviceScopeFactory"></param>
    public CachingProcessingJob(ICachingProcessingConfig config, ILoggerService loggerService, IServiceScopeFactory serviceScopeFactory)
        : base(config.CronExpression, config.TimeZoneInfo, loggerService, serviceScopeFactory)
    {
        _cronExpression = config.CronExpression;
        _getActionUrl = config.GetActionUrl ?? "";
        _postActionUrl = config.PostActionUrl ?? "";
        _filesToCache = config.FilesToCache;
        _pagePaths = config.PageRoutePaths;
        _pdfPaths = config.PdfRoutePaths;
    }

    /// <summary>
    /// Execute caching of the SPA.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: working...");

        var cachingService = GetService<ICachingService>();
        await cachingService.SaveStaticFiles(_filesToCache, _getActionUrl, _postActionUrl);

        if (_pagePaths.Count == 0)
        {
            LoggerService.LogInformation($"{ServiceName}: no routes registered for caching..., quitting the job...");
            return;
        }

        foreach (var pagePath in _pagePaths)
        {
            var page = await cachingService.RenderStaticPage(pagePath.Url, _postActionUrl, pagePath.Name);
            if (!string.IsNullOrWhiteSpace(page))
                LoggerService.LogInformation($"{ServiceName}: page '{pagePath.Name}' has been rendered and saved. Url: '{pagePath.Url}'.");
        }

        if (_pdfPaths.Count == 0)
        {
            LoggerService.LogInformation($"{ServiceName}: no routes registered for generating PDFs..., quitting the job...");
            return;
        }

        foreach (var pdfPath in _pdfPaths)
        {
            var pdf = await cachingService.GeneratePdf(pdfPath.Url, pdfPath.Name);
            if (!string.IsNullOrWhiteSpace(pdf))
                LoggerService.LogInformation($"{ServiceName}: PDF file '{pdfPath.Name}' has been rendered and saved. Url: '{pdfPath.Url}'.");
        }
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: started, CRON expression is '{_cronExpression}'.");

        var staticFileToCache = _filesToCache?.Length ?? 0;
        LoggerService.LogInformation($"{ServiceName}: {staticFileToCache} static file(s) to be cached.");
        if (_filesToCache is not null && _filesToCache.Length > 0)
        {
            foreach (var item in _filesToCache)
            {
                LoggerService.LogInformation($"{ServiceName}: ...to be cached: {item}");
            }
        }

        LoggerService.LogInformation($"{ServiceName}: {_pagePaths.Count} SPA pages to be cached.");
        if (_pagePaths.Count > 0)
        {
            foreach (var item in _pagePaths)
            {
                LoggerService.LogInformation($"{ServiceName}: ...to be cached: {item.Name} (url: {item.Url})");
            }
        }

        LoggerService.LogInformation($"{ServiceName}: {_pdfPaths.Count} pages for PDF printouts.");
        if (_pdfPaths.Count > 0)
        {
            foreach (var item in _pdfPaths)
            {
                LoggerService.LogInformation($"{ServiceName}: ...to be printed to PDF: {item.Name} (url: {item.Url})");
            }
        }

        var cachingService = GetService<ICachingService>();
        await cachingService.GetBrowser();

        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        LoggerService.LogInformation($"{ServiceName}: stopped.");
        return base.StopAsync(cancellationToken);
    }
}