using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendNewsletterCommandHandler : RequestHandler<SendNewsletterCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly AppSettingsModel _appSettings;

    private readonly IMessagingRepository _messagingRepository;

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public SendNewsletterCommandHandler(ILoggerService loggerService, IEmailSenderService emailSenderService, 
        IOptions<AppSettingsModel> options, IMessagingRepository messagingRepository) : base(loggerService)
    {
        _emailSenderService = emailSenderService;
        _messagingRepository = messagingRepository;
        _appSettings = options.Value;
    }

    public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
    {
        var isProduction = CurrentEnv == "Production";
        var deploymentOrigin = _appSettings.PathsDeploymentOrigin;
        var developmentOrigin = _appSettings.PathsDevelopmentOrigin;

        var origin = isProduction ? deploymentOrigin : developmentOrigin;
        var newsletterUpdatePath = _appSettings.PathsNewsletterUpdate;
        var newsletterRemovePath = _appSettings.PathsNewsletterRemove;

        var newsletterUpdateLink = $"{origin}/{request.LanguageId}{newsletterUpdatePath}";
        var newsletterRemoveLink = $"{origin}/{request.LanguageId}{newsletterRemovePath}";

        LoggerService.LogInformation($"Update subscriber base URL: {newsletterUpdateLink}.");
        LoggerService.LogInformation($"Unsubscribe base URL: {newsletterRemoveLink}.");

        if (request.SubscriberInfo is null)
        {
            var message = $"Object: {request.SubscriberInfo} is null. Cannot process newsletter to the subscribers.";
            LoggerService.LogWarning(message);
            return Unit.Value;
        }

        foreach (var subscriber in request.SubscriberInfo)
        {
            var updateSubscriberLink = $"{newsletterUpdateLink}{subscriber.Id}";
            var unsubscribeLink = $"{newsletterRemoveLink}{subscriber.Id}";
            var newValues = new Dictionary<string, string>
            {
                { "{CONTENT}", request.Message },
                { "{UPDATE_EMAIL_LINK}", updateSubscriberLink },
                { "{UNSUBSCRIBE_LINK}", unsubscribeLink }
            };

            var baseUrl = _appSettings.AzStorageBaseUrl;
            var newsletter = _appSettings.PathsTemplatesNewsletter;

            var templateUrl = $"{baseUrl}{newsletter}";
            var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
            LoggerService.LogInformation($"Getting newsletter template from URL: {templateUrl}.");

            var messageId = Guid.NewGuid();
            var payload = new SendMessageConfiguration
            {
                MessageId = messageId,
                From = _appSettings.EmailAddressContact,
                To = new List<string> { subscriber.Email },
                Subject = request.Subject,
                Body = template.MakeBody(newValues)
            };

            await _messagingRepository.CreateServiceBusMessage(messageId);
            await _emailSenderService.SendToServiceBus(payload, cancellationToken);
        }

        return Unit.Value;
    }
}