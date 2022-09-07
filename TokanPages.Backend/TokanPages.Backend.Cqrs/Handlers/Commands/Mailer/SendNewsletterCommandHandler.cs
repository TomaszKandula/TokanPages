namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence.Database;
using Shared.Services;
using Core.Extensions;
using WebApi.Dto.Mailer;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;

public class SendNewsletterCommandHandler : Cqrs.RequestHandler<SendNewsletterCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IApplicationSettings _applicationSettings;
        
    public SendNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var updateSubscriberBaseLink = $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.UpdateSubscriberPath}";
        var unsubscribeBaseLink = $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.UnsubscribePath}";

        LoggerService.LogInformation($"Update subscriber base URL: {updateSubscriberBaseLink}.");
        LoggerService.LogInformation($"Unsubscribe base URL: {unsubscribeBaseLink}.");
            
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

            var baseUrl = _applicationSettings.AzureStorage.BaseUrl;
            var newsletter = _applicationSettings.ApplicationPaths.Templates.Newsletter;

            var templateUrl = $"{baseUrl}{newsletter}";
            var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
            LoggerService.LogInformation($"Getting newsletter template from URL: {templateUrl}.");

            var contact = _applicationSettings.EmailSender.Addresses.Contact;
            var payload = new SenderPayloadDto
            {
                From = contact,
                To = new List<string> { subscriber.Email },
                Subject = request.Subject,
                Body = template.MakeBody(newValues)
            };

            await _emailSenderService.SendEmail(payload, cancellationToken);
        }

        return Unit.Value;
    }
}