namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

using MediatR;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using Database;
using Shared.Models;
using Shared.Services;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Core.Utilities.CustomHttpClient;
using Core.Utilities.CustomHttpClient.Models;

public class SendNewsletterCommandHandler : Cqrs.RequestHandler<SendNewsletterCommand, Unit>
{
    private readonly ICustomHttpClient _customHttpClient;

    private readonly IApplicationSettings _applicationSettings;
        
    public SendNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ICustomHttpClient customHttpClient, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _customHttpClient = customHttpClient;
        _applicationSettings = applicationSettings;
    }

    public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var updateSubscriberBaseLink = $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.UpdateSubscriberPath}";
        var unsubscribeBaseLink = $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.UnsubscribePath}";

        LoggerService.LogInformation($"Get update subscriber base URL: {updateSubscriberBaseLink}.");
        LoggerService.LogInformation($"Get unsubscribe base URL: {unsubscribeBaseLink}.");
            
        foreach (var subscriber in request.SubscriberInfo)
        {
            var updateSubscriberLink = updateSubscriberBaseLink + subscriber.Id;
            var unsubscribeLink = unsubscribeBaseLink + subscriber.Id;
            var newValues = new Dictionary<string, string>
            {
                { "{CONTENT}", request.Message },
                { "{UPDATE_EMAIL_LINK}", updateSubscriberLink },
                { "{UNSUBSCRIBE_LINK}", unsubscribeLink }
            };

            var url = $"{_applicationSettings.AzureStorage.BaseUrl}{Constants.Emails.Templates.Newsletter}";
            LoggerService.LogInformation($"Getting newsletter template from URL: {url}.");

            var configuration = new Configuration { Url = url, Method = "GET" };
            var getTemplate = await _customHttpClient.Execute(configuration, cancellationToken);

            if (getTemplate.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var template = Encoding.Default.GetString(getTemplate.Content);
            var payload = new EmailSenderPayload
            {
                From = Constants.Emails.Addresses.Contact,
                To = new List<string> { subscriber.Email },
                Subject = request.Subject,
                Body = template.MakeBody(newValues)
            };

            var headers = new Dictionary<string, string> { ["X-Private-Key"] = _applicationSettings.EmailSender.PrivateKey };
            configuration = new Configuration { Url = _applicationSettings.EmailSender.BaseUrl, Method = "POST", Headers = headers, 
                StringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.Default, "application/json") };

            var sendEMail = await _customHttpClient.Execute(configuration, cancellationToken);
            if (sendEMail.StatusCode != HttpStatusCode.OK) 
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}");
        }

        return Unit.Value;
    }
}