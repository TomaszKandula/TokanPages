using System.Text;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.EmailSenderService;

public class EmailSenderService : IEmailSenderService
{
    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly IConfiguration _configuration;

    private readonly ILoggerService _loggerService;

    public EmailSenderService(IHttpClientServiceFactory httpClientServiceFactory, IConfiguration configuration, 
        ILoggerService loggerService)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _configuration = configuration;
        _loggerService = loggerService;
    }

    public async Task SendNotification(IEmailConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var origin = _configuration.GetValue<string>("Paths_DeploymentOrigin");
        var baseUrl = _configuration.GetValue<string>("AZ_Storage_BaseUrl");

        var registerFormTemplate = _configuration.GetValue<string>("Paths_Templates_RegisterForm");
        var resetPasswordTemplate = _configuration.GetValue<string>("Paths_Templates_ResetPassword");

        var activationPath = _configuration.GetValue<string>("Paths_Activation");
        var updatePasswordPath = _configuration.GetValue<string>("Paths_UpdatePassword");

        var activationUrl = $"{origin}{activationPath}";
        var updatePasswordUrl = $"{origin}{updatePasswordPath}";

        var subject = configuration switch
        {
            CreateUserConfiguration => "New user registration",
            ResetPasswordConfiguration => "Reset user password",
            _ => ""
        };

        var emailAddress = configuration switch
        {
            CreateUserConfiguration createConfiguration => createConfiguration.EmailAddress,
            ResetPasswordConfiguration resetConfiguration => resetConfiguration.EmailAddress,
            _ => ""
        };

        var templateUrl = configuration switch
        {
            CreateUserConfiguration =>  $"{baseUrl}{registerFormTemplate}",
            ResetPasswordConfiguration => $"{baseUrl}{resetPasswordTemplate}",
            _ => ""
        };

        var templateValues = configuration switch
        {
            CreateUserConfiguration model => new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", $"{activationUrl}{model.ActivationId}" },
                { "{EXPIRATION}", $"{model.ActivationIdEnds}" }
            },
            ResetPasswordConfiguration model => new Dictionary<string, string>
            {
                { "{RESET_LINK}", $"{updatePasswordUrl}{model.ResetId}" },
                { "{EXPIRATION}", $"{model.ExpirationDate}" }
            },
            _ => new Dictionary<string, string>()
        };

        var template = await GetEmailTemplate(templateUrl, cancellationToken);
        var sendFrom = _configuration.GetValue<string>("Email_Address_Contact");
        var payload = new SenderPayloadDto
        {
            From = sendFrom,
            To = new List<string> { emailAddress },
            Subject = subject,
            Body = template.MakeBody(templateValues)
        };

        await SendEmail(payload, cancellationToken);
    }

    public async Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default)
    {
        var httpConfiguration = new Configuration { Url = templateUrl, Method = "GET" };
        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute(httpConfiguration, cancellationToken);

        if (result.Content == null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

        return Encoding.Default.GetString(result.Content);
    }

    public async Task SendEmail(object content, CancellationToken cancellationToken = default)
    {
        var headers = new Dictionary<string, string>
        {
            ["X-Private-Key"] = _configuration.GetValue<string>("Email_PrivateKey")
        };

        var payload = new ContentString { Payload = content };
        var configuration = new Configuration 
        { 
            Url = _configuration.GetValue<string>("Email_BaseUrl"), 
            Method = "POST", 
            Headers = headers,
            PayloadContent = payload
        };

        var client = _httpClientServiceFactory.Create(true, _loggerService);
        await client.Execute(configuration, cancellationToken);
    }
}