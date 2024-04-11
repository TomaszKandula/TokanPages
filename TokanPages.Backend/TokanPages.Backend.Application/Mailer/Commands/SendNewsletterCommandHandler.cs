using MediatR;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;

namespace TokanPages.Backend.Application.Mailer.Commands;

public class SendNewsletterCommandHandler : RequestHandler<SendNewsletterCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IConfiguration _configuration;

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public SendNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _configuration = configuration;
    }

    public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
    {
        var isProduction = CurrentEnv == "Production";
        var deploymentOrigin = _configuration.GetValue<string>("Paths_DeploymentOrigin");
        var developmentOrigin = _configuration.GetValue<string>("Paths_DevelopmentOrigin");

        var origin = isProduction ? deploymentOrigin : developmentOrigin;
        var updateSubscriberPath = _configuration.GetValue<string>("Paths_UpdateSubscriber");
        var unsubscribePath = _configuration.GetValue<string>("Paths_Unsubscribe");

        var updateSubscriberBaseLink = $"{origin}{updateSubscriberPath}";
        var unsubscribeBaseLink = $"{origin}{unsubscribePath}";

        LoggerService.LogInformation($"Update subscriber base URL: {updateSubscriberBaseLink}.");
        LoggerService.LogInformation($"Unsubscribe base URL: {unsubscribeBaseLink}.");

        if (request.SubscriberInfo is null)
        {
            var message = $"Object: {request.SubscriberInfo} is null. Cannot process newsletter to the subscribers.";
            LoggerService.LogWarning(message);
            return Unit.Value;
        }

        foreach (var subscriber in request.SubscriberInfo)
        {
            var updateSubscriberLink = $"{updateSubscriberBaseLink}{subscriber.Id}";
            var unsubscribeLink = $"{unsubscribeBaseLink}{subscriber.Id}";
            var newValues = new Dictionary<string, string>
            {
                { "{CONTENT}", request.Message },
                { "{UPDATE_EMAIL_LINK}", updateSubscriberLink },
                { "{UNSUBSCRIBE_LINK}", unsubscribeLink }
            };

            var baseUrl = _configuration.GetValue<string>("AZ_Storage_BaseUrl");
            var newsletter = _configuration.GetValue<string>("Paths_Templates_Newsletter");

            var templateUrl = $"{baseUrl}{newsletter}";
            var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
            LoggerService.LogInformation($"Getting newsletter template from URL: {templateUrl}.");

            var messageId = Guid.NewGuid();
            var serviceBusMessage = new ServiceBusMessage
            {
                Id = messageId,
                IsConsumed = false
            };

            var payload = new SendMessageConfiguration
            {
                MessageId = messageId,
                From = _configuration.GetValue<string>("Email_Address_Contact"),
                To = new List<string> { subscriber.Email },
                Subject = request.Subject,
                Body = template.MakeBody(newValues)
            };

            await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
            await _emailSenderService.SendToServiceBus(payload, cancellationToken);
        }

        return Unit.Value;
    }
}