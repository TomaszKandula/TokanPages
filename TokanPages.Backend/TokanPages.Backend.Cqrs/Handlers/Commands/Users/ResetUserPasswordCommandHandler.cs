namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using MediatR;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class ResetUserPasswordCommandHandler : TemplateHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ILogger _logger;

        private readonly ICustomHttpClient _customHttpClient;

        private readonly ISmtpClientService _smtpClientService;

        private readonly ITemplateService _templateService;

        private readonly IDateTimeService _dateTimeService; 

        private readonly AzureStorage _azureStorage;

        private readonly ApplicationPaths _applicationPaths;

        private readonly ExpirationSettings _expirationSettings;

        public ResetUserPasswordCommandHandler(DatabaseContext databaseContext, ILogger logger, ICustomHttpClient customHttpClient, 
            ISmtpClientService smtpClientService, ITemplateService templateService, IDateTimeService dateTimeService, 
            AzureStorage azureStorage, ApplicationPaths applicationPaths, ExpirationSettings expirationSettings)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _customHttpClient = customHttpClient;
            _smtpClientService = smtpClientService;
            _templateService = templateService;
            _dateTimeService = dateTimeService;
            _azureStorage = azureStorage;
            _applicationPaths = applicationPaths;
            _expirationSettings = expirationSettings;
        }

        public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var users = await _databaseContext.Users
                .Where(users => users.EmailAddress == request.EmailAddress)
                .ToListAsync(cancellationToken);

            if (!users.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var currentUser = users.First();
            var resetId = Guid.NewGuid();
            var expirationDate = _dateTimeService.Now.AddMinutes(_expirationSettings.ResetIdExpiresIn);
            currentUser.CryptedPassword = string.Empty;
            currentUser.ResetId = resetId;
            currentUser.ResetIdEnds = expirationDate;
            currentUser.LastUpdated = _dateTimeService.Now;
            await _databaseContext.SaveChangesAsync(cancellationToken);
            await SendNotification(request.EmailAddress, resetId, expirationDate, cancellationToken);

            return await Task.FromResult(Unit.Value);
        }

        private async Task SendNotification(string emailAddress, Guid resetId, DateTime expirationDate, CancellationToken cancellationToken)
        {
            _smtpClientService.From = Constants.Emails.Addresses.Contact;
            _smtpClientService.Tos = new List<string> { emailAddress };
            _smtpClientService.Subject = "Reset user password";

            var resetLink = $"{_applicationPaths.DeploymentOrigin}{_applicationPaths.UpdatePasswordPath}{resetId}";
            
            var newValues = new Dictionary<string, string>
            {
                { "{RESET_LINK}", resetLink },
                { "{EXPIRATION}", $"{expirationDate}" }
            };

            var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.ResetPassword}";
            _logger.LogInformation($"Getting email template from URL: {url}.");

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
    }
}