using System.Diagnostics;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services.Base;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.VideoProcessingService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TokanPages.HostedServices.Services;

/// <summary>
/// Implementation of video processing hosted service.
/// </summary>
public class VideoProcessing : Processing
{
    /// <summary>
    /// Queue name for video processing message.
    /// </summary>
    protected override string QueueName { get; set; } = "video_queue";

    private readonly IVideoProcessor _videoProcessor;

    private readonly DatabaseContext _databaseContext;

    /// <summary>
    /// Implementation of video processing hosted service.
    /// </summary>
    /// <param name="loggerService">Logger Service instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="videoProcessor">Video Processor instance.</param>
    /// <param name="databaseContext">Database instance.</param>
    public VideoProcessing(ILoggerService loggerService, IAzureBusFactory azureBusFactory, 
        IVideoProcessor videoProcessor, DatabaseContext databaseContext) : base(loggerService, azureBusFactory)
    {
        _videoProcessor = videoProcessor;
        _databaseContext = databaseContext;
    }

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

        LoggerService.LogInformation($"{nameof(VideoProcessing)} has been called");
        LoggerService.LogInformation($"Received message with ID: {args.Message.MessageId}");

        var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        if (currentEnv != request.TargetEnv)
        {
            LoggerService.LogInformation($"Current environment: {currentEnv}");
            LoggerService.LogInformation($"Target environment: {request.TargetEnv}");
            LoggerService.LogInformation("Different target environment, quitting the job...");
            return;
        }

        var canContinue = await CanContinue(request.MessageId, CancellationToken.None);
        if (!canContinue)
        {
            LoggerService.LogInformation($"Message ID ({request.MessageId}) has been already processed, quitting the job...");
            return;
        }

        var timer = new Stopwatch();
        timer.Start();
        LoggerService.LogInformation("Video processing started...");

        await _videoProcessor.Process(request).ConfigureAwait(false);
        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"Video processed within: {timer.Elapsed}");
    }

    private async Task<bool> CanContinue(Guid messageId, CancellationToken cancellationToken)
    {
        var busMessages = await _databaseContext.ServiceBusMessages
            .Where(messages => messages.Id == messageId)
            .SingleOrDefaultAsync(cancellationToken);

        if (busMessages is null || busMessages.IsConsumed)
            return false;

        busMessages.IsConsumed = true;
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}