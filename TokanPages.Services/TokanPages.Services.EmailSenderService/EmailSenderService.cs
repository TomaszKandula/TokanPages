using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.AzureBusService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.EmailSenderService;

[ExcludeFromCodeCoverage]
internal sealed class EmailSenderService : IEmailSenderService
{
    private const string QueueName = "email_queue";

    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly IAzureBusFactory _azureBusFactory;

    private readonly AppSettingsModel _appSettings;

    private readonly ILoggerService _loggerService;

    private readonly IJsonSerializer _jsonSerializer;

    private static JsonSerializerSettings Settings => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

    private static string CurrentEnv => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing";

    public EmailSenderService(IHttpClientServiceFactory httpClientServiceFactory, IOptions<AppSettingsModel> configuration, 
        ILoggerService loggerService, IAzureBusFactory azureBusFactory, IJsonSerializer jsonSerializer)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _appSettings = configuration.Value;
        _loggerService = loggerService;
        _azureBusFactory = azureBusFactory;
        _jsonSerializer = jsonSerializer;
    }

    public async Task SendNotification(IEmailConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        var deploymentOrigin = _appSettings.PathsDeploymentOrigin;
        var developmentOrigin = _appSettings.PathsDevelopmentOrigin;

        var origin = isProduction ? deploymentOrigin : developmentOrigin;
        var baseUrl = _appSettings.AzStorageBaseUrl;

        var registerFormTemplate = _appSettings.PathsTemplatesRegisterForm;
        var resetPasswordTemplate = _appSettings.PathsTemplatesResetPassword;
        var verifyEmailTemplate = _appSettings.PathsTemplatesVerifyEmail;

        var activationPath = _appSettings.PathsActivation;
        var updatePasswordPath = _appSettings.PathsUpdatePassword;

        var subject = configuration switch
        {
            CreateUserConfiguration => "New user registration",
            ResetPasswordConfiguration => "Reset user password",
            VerifyEmailConfiguration => "Verify email address",
            _ => ""
        };

        var emailAddress = configuration switch
        {
            CreateUserConfiguration createConfiguration => createConfiguration.EmailAddress,
            ResetPasswordConfiguration resetConfiguration => resetConfiguration.EmailAddress,
            VerifyEmailConfiguration model => model.EmailAddress,
            _ => ""
        };

        var templateUrl = configuration switch
        {
            CreateUserConfiguration =>  $"{baseUrl}{registerFormTemplate}",
            ResetPasswordConfiguration => $"{baseUrl}{resetPasswordTemplate}",
            VerifyEmailConfiguration => $"{baseUrl}{verifyEmailTemplate}",
            _ => ""
        };

        var templateValues = configuration switch
        {
            CreateUserConfiguration model => new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", $"{origin}/{model.LanguageId}{activationPath}{model.ActivationId}" },
                { "{EXPIRATION}", $"{model.ActivationIdEnds}" }
            },
            ResetPasswordConfiguration model => new Dictionary<string, string>
            {
                { "{RESET_LINK}", $"{origin}/{model.LanguageId}{updatePasswordPath}{model.ResetId}" },
                { "{EXPIRATION}", $"{model.ExpirationDate}" }
            },
            VerifyEmailConfiguration model => new Dictionary<string, string>
            {
                { "{VERIFICATION_LINK}", $"{origin}/{model.LanguageId}{activationPath}{model.VerificationId}" },
                { "{EXPIRATION}", $"{model.VerificationIdEnds}" }
            },
            _ => new Dictionary<string, string>()
        };

        var template = await GetEmailTemplate(templateUrl, cancellationToken);
        var sendFrom = _appSettings.EmailAddressContact;
        var payload = new SendMessageConfiguration
        {
            From = sendFrom,
            To = new List<string> { emailAddress },
            Subject = subject,
            Body = template.MakeBody(templateValues)
        };

        await SendEmail(payload, cancellationToken);
    }

    public async Task SendEmail(object content, CancellationToken cancellationToken = default)
    {
        var headers = new Dictionary<string, string>
        {
            ["X-Private-Key"] = _appSettings.EmailPrivateKey
        };

        var payload = new ContentString { Payload = content };
        var configuration = new HttpClientSettings 
        { 
            Url = _appSettings.EmailBaseUrl, 
            Method = "POST", 
            Headers = headers,
            PayloadContent = payload
        };

        var client = _httpClientServiceFactory.Create(true, _loggerService);
        await client.Execute(configuration, cancellationToken);
    }

    public async Task SendToServiceBus(IEmailConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var data = configuration switch
        {
            CreateUserConfiguration item => new RequestEmailProcessing { TargetEnv = CurrentEnv, CreateUserConfiguration = item },
            ResetPasswordConfiguration item => new RequestEmailProcessing { TargetEnv = CurrentEnv, ResetPasswordConfiguration = item },
            VerifyEmailConfiguration item => new RequestEmailProcessing { TargetEnv = CurrentEnv, VerifyEmailConfiguration = item },
            SendMessageConfiguration item => new RequestEmailProcessing { TargetEnv = CurrentEnv, SendMessageConfiguration = item },
            _ => throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED)
        };

        var serialized = _jsonSerializer.Serialize(data, Formatting.None, Settings);
        var messages = new List<string> { serialized };

        _loggerService.LogInformation($"[{QueueName} | {CurrentEnv}]: Send message to service bus, size: '{serialized.Length}' bytes.");

        var busService = _azureBusFactory.Create();
        await busService.SendMessages(QueueName, messages, cancellationToken);
    }
    
    public async Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default)
    {
        var httpConfiguration = new HttpClientSettings { Url = templateUrl, Method = "GET" };
        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute(httpConfiguration, cancellationToken);

        if (result.Content == null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

        return Encoding.Default.GetString(result.Content);
    }
}