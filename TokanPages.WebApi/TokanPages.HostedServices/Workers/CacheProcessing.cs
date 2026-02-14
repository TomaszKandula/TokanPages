using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.HostedServices.Base;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.SpaCachingService.Abstractions;
using TokanPages.Services.SpaCachingService.Models;

namespace TokanPages.HostedServices.Workers;

/// <summary>
/// Implementation of SPA cache processing hosted service.
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheProcessing : Processing
{
    /// <summary>
    /// Queue name for SPA cache processing message.
    /// </summary>
    protected override string QueueName { get; set; } = "cache_queue";

    private const string ServiceName = $"[{nameof(CacheProcessing)}]";

    /// <summary>
    /// Implementation of cache processing hosted service.
    /// </summary>
    /// <param name="loggerService">Logger Service instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="messagingRepository">Messaging repository instance.</param>
    /// <param name="serviceScopeFactory">Service scope factory instance.</param>
    public CacheProcessing(ILoggerService loggerService, IAzureBusFactory azureBusFactory,
        IMessagingRepository messagingRepository, IServiceScopeFactory serviceScopeFactory)
        : base(loggerService, azureBusFactory, messagingRepository, serviceScopeFactory) { }

    /// <summary>
    /// Custom implementation for cache processing.
    /// </summary>
    /// <param name="args"></param>
    public override async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        var data = args.Message.Body.ToString();
        var request = JsonConvert.DeserializeObject<RequestCacheProcessing>(data);
        if (request is null)
            throw new GeneralException(ErrorNoRequestBody);

        LoggerService.LogInformation($"{ServiceName}: Received message with ID: {args.Message.MessageId}");

        var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        if (currentEnv != request.TargetEnv)
        {
            LoggerService.LogInformation($"{ServiceName}: Current environment: {currentEnv}");
            LoggerService.LogInformation($"{ServiceName}: Target environment: {request.TargetEnv}");
            LoggerService.LogInformation($"{ServiceName}: Different target environment, quitting the job...");
            return;
        }

        var canContinue = await CanContinue(request.MessageId);
        if (!canContinue)
        {
            LoggerService.LogInformation($"{ServiceName}: Message ID ({request.MessageId}) has been already processed, quitting the job...");
            return;
        }

        var timer = new Stopwatch();
        timer.Start();
        LoggerService.LogInformation($"{ServiceName}: SPA cache processing started...");

        var cachingService = GetService<ICachingService>();

        if (request.Files is not null && request.Files.Length > 0)
        {
            await cachingService.SaveStaticFiles(request.Files, request.GetUrl, request.PostUrl).ConfigureAwait(false);
        }

        if (request.Paths.Count > 0)
        {
            foreach (var path in request.Paths)
            {
                var page = await cachingService.RenderStaticPage(path.Url, request.PostUrl, path.Name).ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(page))
                    LoggerService.LogInformation($"{ServiceName}: page '{path.Name}' has been rendered and saved. Url: '{path.Url}'.");
            }
        }

        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"{ServiceName}: SPA cache processed within: {timer.Elapsed}");
    }
}