using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Base;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.VideoProcessingService.Abstractions;
using TokanPages.Services.VideoProcessingService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TokanPages.Persistence.Database.Contexts;

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

        LoggerService.LogInformation($"{ServiceName}: Received message with ID: {args.Message.MessageId}");

        var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        if (currentEnv != request.TargetEnv)
        {
            LoggerService.LogInformation($"{ServiceName}: Current environment: {currentEnv}");
            LoggerService.LogInformation($"{ServiceName}: Target environment: {request.TargetEnv}");
            LoggerService.LogInformation($"{ServiceName}: Different target environment, quitting the job...");
            return;
        }

        var canContinue = await CanContinue(request.MessageId, CancellationToken.None);
        if (!canContinue)
        {
            LoggerService.LogInformation($"{ServiceName}: Message ID ({request.MessageId}) has been already processed, quitting the job...");
            return;
        }

        var timer = new Stopwatch();
        timer.Start();
        LoggerService.LogInformation($"{ServiceName}: Video processing started...");

        await _videoProcessor.Process(request).ConfigureAwait(false);
        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"{ServiceName}: Video processed within: {timer.Elapsed}");
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