using System.Diagnostics;
using Azure.Messaging.ServiceBus;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services.Base;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TokanPages.HostedServices.Services;

/// <summary>
/// Implementation of email processing hosted service.
/// </summary>
public class EmailProcessing : Processing
{
    /// <summary>
    /// Queue name for email processing message.
    /// </summary>
    protected override string QueueName { get; set; } = "email_queue";

    private readonly IEmailSenderService _emailSenderService;

    private readonly DatabaseContext _databaseContext;

    /// <summary>
    /// Implementation of email processing hosted service.
    /// </summary>
    /// <param name="loggerService">Logger Service instance.</param>
    /// <param name="azureBusFactory">Azure Bus Factory instance.</param>
    /// <param name="emailSenderService">Email Sender Service instance.</param>
    /// <param name="databaseContext">Database instance.</param>
    public EmailProcessing(ILoggerService loggerService, IAzureBusFactory azureBusFactory, 
        IEmailSenderService emailSenderService, DatabaseContext databaseContext) : base(loggerService, azureBusFactory)
    {
        _emailSenderService = emailSenderService;
        _databaseContext = databaseContext;
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

        LoggerService.LogInformation($"{nameof(EmailProcessing)} has been called");
        LoggerService.LogInformation($"Received message with ID: {args.Message.MessageId}");

        var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";
        if (currentEnv != request.TargetEnv)
        {
            LoggerService.LogInformation($"Current environment: {currentEnv}");
            LoggerService.LogInformation($"Target environment: {request.TargetEnv}");
            LoggerService.LogInformation("Different target environment, quitting the job...");
            return;
        }

        var timer = new Stopwatch();
        timer.Start();

        LoggerService.LogInformation("Email processing started...");
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
            LoggerService.LogInformation("No configuration have been found...");
        }

        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

        timer.Stop();
        LoggerService.LogInformation($"Email processed within: {timer.Elapsed}");
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

    private async Task NotificationForConfiguration<T>(T configuration) where T : IEmailConfiguration
    {
        var messageId = configuration.MessageId;
        var canContinue = await CanContinue(messageId, CancellationToken.None);
        if (canContinue)
        {
            LoggerService.LogInformation($"Received: {nameof(configuration)}..., using: SendNotification method...");
            await _emailSenderService.SendNotification(configuration, CancellationToken.None).ConfigureAwait(false);
        }
        else
        {
            LoggerService.LogInformation($"Message ID ({messageId}) has been already processed, quitting the job...");
        }
    }

    private async Task EmailForConfiguration<T>(T configuration) where T : IEmailConfiguration
    {
        var messageId = configuration.MessageId;
        var canContinue = await CanContinue(messageId, CancellationToken.None);
        if (canContinue)
        {
            LoggerService.LogInformation($"Received: {nameof(configuration)}..., using: SendEmail method...");
            await _emailSenderService.SendEmail(configuration, CancellationToken.None).ConfigureAwait(false);
        }
        else
        {
            LoggerService.LogInformation($"Message ID ({messageId}) has been already processed, quitting the job...");
        }
    }
}