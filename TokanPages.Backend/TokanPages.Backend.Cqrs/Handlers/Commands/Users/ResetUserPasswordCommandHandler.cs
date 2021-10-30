namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using MediatR;
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.LoggerService;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class ResetUserPasswordCommandHandler : TemplateHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly ICustomHttpClient _customHttpClient;

        private readonly ITemplateService _templateService;

        private readonly IDateTimeService _dateTimeService; 

        private readonly AzureStorage _azureStorage;

        private readonly ApplicationPaths _applicationPaths;

        private readonly ExpirationSettings _expirationSettings;

        private readonly EmailSender _emailSender;

        private Configuration _configuration;

        public ResetUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
            ICustomHttpClient customHttpClient, ITemplateService templateService, IDateTimeService dateTimeService, 
            AzureStorage azureStorage, ApplicationPaths applicationPaths, ExpirationSettings expirationSettings,
                EmailSender emailSender) : base(databaseContext, loggerService)
        {
            _customHttpClient = customHttpClient;
            _templateService = templateService;
            _dateTimeService = dateTimeService;
            _azureStorage = azureStorage;
            _applicationPaths = applicationPaths;
            _expirationSettings = expirationSettings;
            _emailSender = emailSender;
        }

        public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var users = await DatabaseContext.Users
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
            await DatabaseContext.SaveChangesAsync(cancellationToken);
            await SendNotification(request.EmailAddress, resetId, expirationDate, cancellationToken);

            return await Task.FromResult(Unit.Value);
        }

        private async Task SendNotification(string emailAddress, Guid resetId, DateTime expirationDate, CancellationToken cancellationToken)
        {
            var resetLink = $"{_applicationPaths.DeploymentOrigin}{_applicationPaths.UpdatePasswordPath}{resetId}";
            var newValues = new Dictionary<string, string>
            {
                { "{RESET_LINK}", resetLink },
                { "{EXPIRATION}", $"{expirationDate}" }
            };

            var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.ResetPassword}";
            LoggerService.LogInformation($"Getting email template from URL: {url}.");

            _configuration = new Configuration { Url = url, Method = "GET" };
            var getTemplate = await _customHttpClient.Execute(_configuration, cancellationToken);

            if (getTemplate.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var template = Encoding.Default.GetString(getTemplate.Content);
            var payload = new EmailSenderPayload
            {
                PrivateKey = _emailSender.PrivateKey,
                From = Constants.Emails.Addresses.Contact,
                To = new List<string> { emailAddress },
                Subject = "Reset user password",
                Body = _templateService.MakeBody(template, newValues)
            };            

            _configuration = new Configuration { Url = _emailSender.BaseUrl, Method = "POST", StringContent = 
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.Default, "application/json") };

            var sendEmail = await _customHttpClient.Execute(_configuration, cancellationToken);
            if (sendEmail.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}");
        }
    }
}