using MediatR;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.WebApi.Dto.Mailer;

namespace TokanPages.Backend.Application.Mailer.Commands;

public class SendNewsletterCommandHandler : RequestHandler<SendNewsletterCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IConfiguration _configuration;

    public SendNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _configuration = configuration;
    }

    public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken)
    {
        var deploymentOrigin = _configuration.GetValue<string>("");
        var updateSubscriberPath = _configuration.GetValue<string>("");
        var unsubscribePath = _configuration.GetValue<string>("");

        var updateSubscriberBaseLink = $"{deploymentOrigin}{updateSubscriberPath}";
        var unsubscribeBaseLink = $"{deploymentOrigin}{unsubscribePath}";

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

            var baseUrl = _configuration.GetValue<string>("AZ_Storage_BaseUrl");
            var newsletter = _configuration.GetValue<string>("Paths_Templates_Newsletter");

            var templateUrl = $"{baseUrl}{newsletter}";
            var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
            LoggerService.LogInformation($"Getting newsletter template from URL: {templateUrl}.");

            var contact = _configuration.GetValue<string>("Email_Address_Contact");
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