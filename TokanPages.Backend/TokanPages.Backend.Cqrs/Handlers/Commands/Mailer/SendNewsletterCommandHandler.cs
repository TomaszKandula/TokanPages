namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class SendNewsletterCommandHandler : TemplateHandler<SendNewsletterCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly ICustomHttpClient FCustomHttpClient;
        
        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateService FTemplateService;
        
        private readonly AzureStorage FAzureStorage;
        
        private readonly ApplicationPaths FApplicationPaths;

        public SendNewsletterCommandHandler(ILogger ALogger, ICustomHttpClient ACustomHttpClient, ISmtpClientService ASmtpClientService, 
            ITemplateService ATemplateService, AzureStorage AAzureStorage, ApplicationPaths AApplicationPaths)
        {
            FLogger = ALogger;
            FCustomHttpClient = ACustomHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateService = ATemplateService;
            FAzureStorage = AAzureStorage;
            FApplicationPaths = AApplicationPaths;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LUpdateSubscriberBaseLink = FApplicationPaths.DeploymentOrigin + FApplicationPaths.UpdateSubscriberPath;
            var LUnsubscribeBaseLink = FApplicationPaths.DeploymentOrigin + FApplicationPaths.UnsubscribePath;

            FLogger.LogInformation($"Update subscriber base URL: {LUpdateSubscriberBaseLink}.");
            FLogger.LogInformation($"Unsubscribe base URL: {LUnsubscribeBaseLink}.");
            
            foreach (var LSubscriber in ARequest.SubscriberInfo)
            {
                FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
                FSmtpClientService.Tos = new List<string> { LSubscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var LUpdateSubscriberLink = LUpdateSubscriberBaseLink + LSubscriber.Id;
                var LUnsubscribeLink = LUnsubscribeBaseLink + LSubscriber.Id;
                var LNewValues = new Dictionary<string, string>
                {
                    { "{CONTENT}", ARequest.Message },
                    { "{UPDATE_EMAIL_LINK}", LUpdateSubscriberLink },
                    { "{UNSUBSCRIBE_LINK}", LUnsubscribeLink }
                };

                var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.NEWSLETTER}";
                FLogger.LogInformation($"Getting newsletter template from URL: {LUrl}.");
                
                var LConfiguration = new Configuration { Url = LUrl, Method = "GET" };
                var LResults = await FCustomHttpClient.Execute(LConfiguration, ACancellationToken);

                if (LResults.Content == null)
                    throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

                var LTemplate = System.Text.Encoding.Default.GetString(LResults.Content);
                FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

                var LResult = await FSmtpClientService.Send(ACancellationToken);
                if (!LResult.IsSucceeded) 
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}