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
        private readonly ILogger _logger;

        private readonly ICustomHttpClient _customHttpClient;
        
        private readonly ISmtpClientService _smtpClientService;
        
        private readonly ITemplateService _templateService;
        
        private readonly AzureStorage _azureStorage;
        
        private readonly ApplicationPaths _applicationPaths;

        public SendNewsletterCommandHandler(ILogger logger, ICustomHttpClient customHttpClient, ISmtpClientService smtpClientService, 
            ITemplateService templateService, AzureStorage azureStorage, ApplicationPaths applicationPaths)
        {
            _logger = logger;
            _customHttpClient = customHttpClient;
            _smtpClientService = smtpClientService;
            _templateService = templateService;
            _azureStorage = azureStorage;
            _applicationPaths = applicationPaths;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand request, CancellationToken cancellationToken) 
        {
            var updateSubscriberBaseLink = _applicationPaths.DeploymentOrigin + _applicationPaths.UpdateSubscriberPath;
            var unsubscribeBaseLink = _applicationPaths.DeploymentOrigin + _applicationPaths.UnsubscribePath;

            _logger.LogInformation($"Update subscriber base URL: {updateSubscriberBaseLink}.");
            _logger.LogInformation($"Unsubscribe base URL: {unsubscribeBaseLink}.");
            
            foreach (var subscriber in request.SubscriberInfo)
            {
                _smtpClientService.From = Constants.Emails.Addresses.Contact;
                _smtpClientService.Tos = new List<string> { subscriber.Email };
                _smtpClientService.Bccs = null;
                _smtpClientService.Subject = request.Subject;

                var updateSubscriberLink = updateSubscriberBaseLink + subscriber.Id;
                var unsubscribeLink = unsubscribeBaseLink + subscriber.Id;
                var newValues = new Dictionary<string, string>
                {
                    { "{CONTENT}", request.Message },
                    { "{UPDATE_EMAIL_LINK}", updateSubscriberLink },
                    { "{UNSUBSCRIBE_LINK}", unsubscribeLink }
                };

                var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.Newsletter}";
                _logger.LogInformation($"Getting newsletter template from URL: {url}.");
                
                var configuration = new Configuration { Url = url, Method = "GET" };
                var results = await _customHttpClient.Execute(configuration, cancellationToken);

                if (results.Content == null)
                    throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

                var template = System.Text.Encoding.Default.GetString(results.Content);
                _smtpClientService.HtmlBody = _templateService.MakeBody(template, newValues);

                var result = await _smtpClientService.Send(cancellationToken);
                if (!result.IsSucceeded) 
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {result.ErrorDesc}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}