using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.HostedServices.Base;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.VideoProcessingService.Models;
using Newtonsoft.Json;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;

namespace TokanPages.HostedServices.Workers;

/// <summary>
/// Implementation of video processing hosted service.
/// </summary>
[ExcludeFromCodeCoverage]
public class VideoProcessing : Processing
{
    /// <summary>
    /// Queue name for video processing message.
    /// </summary>
    protected override string QueueName { get; set; } = "video_queue";

    private const string ServiceName = $"[{nameof(VideoProcessing)}]";

    /// <summary>
    /// Implementation of video processing hosted service.
    /// </summary>
    /// <param name="loggerService">Logger Service instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="messagingRepository">Messaging repository instance.</param>
    /// <param name="serviceScopeFactory">Service scope factory instance.</param>
    public VideoProcessing(ILoggerService loggerService, IAzureBusFactory azureBusFactory, 
        IMessagingRepository messagingRepository, IServiceScopeFactory serviceScopeFactory) 
        : base(loggerService, azureBusFactory, messagingRepository, serviceScopeFactory) { }

    /// <summary>
    /// Custom implementation for video processing.
    /// </summary>
    /// <param name="args"></param>
    public override async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        var data = args.Message.Body.ToString();
        var request = JsonConvert.DeserializeObject<RequestVideoProcessing>(data);
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
        LoggerService.LogInformation($"{ServiceName}: Video processing started...");

        var videoProcessor = GetService<IVideoProcessor>();
        await videoProcessor.Process(request).ConfigureAwait(false);
        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"{ServiceName}: Video processed within: {timer.Elapsed}");
    }
}