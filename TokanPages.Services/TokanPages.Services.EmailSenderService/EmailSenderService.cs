using System.Text;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.ApplicationSettings;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.WebApi.Dto.Mailer;

namespace TokanPages.Services.EmailSenderService;

public class EmailSenderService : IEmailSenderService
{
    private readonly IHttpClientService _httpClientService;

    private readonly IApplicationSettings _applicationSettings;

    public EmailSenderService(IHttpClientService httpClientService, IApplicationSettings applicationSettings)
    {
        _httpClientService = httpClientService;
        _applicationSettings = applicationSettings;
    }

    public async Task SendNotification(IEmailConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var origin = _applicationSettings.ApplicationPaths.DeploymentOrigin;
        var baseUrl = _applicationSettings.AzureStorage.BaseUrl;

        var registerFormTemplate = _applicationSettings.ApplicationPaths.Templates.RegisterForm;
        var resetPasswordTemplate = _applicationSettings.ApplicationPaths.Templates.ResetPassword;

        var activationPath = _applicationSettings.ApplicationPaths.ActivationPath;
        var updatePasswordPath = _applicationSettings.ApplicationPaths.UpdatePasswordPath;

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
        var sendFrom = _applicationSettings.EmailSender.Addresses.Contact;
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
        var result = await _httpClientService.Execute(httpConfiguration, cancellationToken);

        if (result.Content == null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

        return Encoding.Default.GetString(result.Content);
    }

    public async Task SendEmail(object content, CancellationToken cancellationToken = default)
    {
        var headers = new Dictionary<string, string>
        {
            ["X-Private-Key"] = _applicationSettings.EmailSender.PrivateKey
        };

        var payload = new ContentString { Payload = content };
        var configuration = new Configuration 
        { 
            Url = _applicationSettings.EmailSender.BaseUrl, 
            Method = "POST", 
            Headers = headers,
            PayloadContent = payload
        };

        await _httpClientService.Execute(configuration, cancellationToken);
    }
}