using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.HostedServices.Base;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using Newtonsoft.Json;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;

namespace TokanPages.HostedServices.Workers;

/// <summary>
/// Implementation of email processing hosted service.
/// </summary>
[ExcludeFromCodeCoverage]
public class EmailProcessing : Processing
{
    /// <summary>
    /// Queue name for email processing message.
    /// </summary>
    protected override string QueueName { get; set; } = "email_queue";

    private const string ServiceName = $"[{nameof(EmailProcessing)}]";

    private readonly IEmailSenderService _emailSenderService;

    private readonly IMessagingRepository _messagingRepository;

    /// <summary>
    /// Implementation of email processing hosted service.
    /// </summary>
    /// <param name="loggerService">Logger Service instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="emailSenderService">Email Sender Service instance.</param>
    /// <param name="messagingRepository">Messaging repository instance.</param>
    public EmailProcessing(ILoggerService loggerService, IAzureBusFactory azureBusFactory, 
        IEmailSenderService emailSenderService, IMessagingRepository messagingRepository) : base(loggerService, azureBusFactory)
    {
        _emailSenderService = emailSenderService;
        _messagingRepository = messagingRepository;
    }

    /// <summary>
    /// Custom implementation for email processing.
    /// </summary>
    /// <param name="args"></param>
    public override async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        var data = args.Message.Body.ToString();
        var request = JsonConvert.DeserializeObject<RequestEmailProcessing>(data);
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

        var timer = new Stopwatch();
        timer.Start();

        LoggerService.LogInformation($"{ServiceName}: Email processing started...");
        if (request.CreateUserConfiguration is not null)
        {
            await NotificationForConfiguration(request.CreateUserConfiguration);
        } 
        else if (request.ResetPasswordConfiguration is not null)
        {
            await NotificationForConfiguration(request.ResetPasswordConfiguration);
        }
        else if (request.VerifyEmailConfiguration is not null)
        {
            await NotificationForConfiguration(request.VerifyEmailConfiguration);
        }
        else if (request.SendMessageConfiguration is not null)
        {
            await EmailForConfiguration(request.SendMessageConfiguration);
        }
        else
        {
            LoggerService.LogInformation($"{ServiceName}: No configuration have been found...");
        }

        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"{ServiceName}: Email processed within: {timer.Elapsed}");
    }

    private async Task<bool> CanContinue(Guid messageId)
    {
        var busMessages = await _messagingRepository.GetServiceBusMessage(messageId);
        if (busMessages is null)
            return false;

        await _messagingRepository.DeleteServiceBusMessage(messageId);
        return true;
    }

    private async Task NotificationForConfiguration<T>(T configuration) where T : IEmailConfiguration
    {
        var messageId = configuration.MessageId;
        var canContinue = await CanContinue(messageId);
        if (canContinue)
        {
            LoggerService.LogInformation($"{ServiceName}: Received: {nameof(configuration)}..., using: SendNotification method...");
            await _emailSenderService.SendNotification(configuration, CancellationToken.None).ConfigureAwait(false);
        }
        else
        {
            LoggerService.LogInformation($"{ServiceName}: Message ID ({messageId}) has been already processed, quitting the job...");
        }
    }

    private async Task EmailForConfiguration<T>(T configuration) where T : IEmailConfiguration
    {
        var messageId = configuration.MessageId;
        var canContinue = await CanContinue(messageId);
        if (canContinue)
        {
            LoggerService.LogInformation($"{ServiceName}: Received: {nameof(configuration)}..., using: SendEmail method...");
            await _emailSenderService.SendEmail(configuration, CancellationToken.None).ConfigureAwait(false);
        }
        else
        {
            LoggerService.LogInformation($"{ServiceName}: Message ID ({messageId}) has been already processed, quitting the job...");
        }
    }
}