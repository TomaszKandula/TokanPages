namespace TokanPages.Services.EmailSenderService;

using System.Net;
using System.Text;
using Models;
using Models.Interfaces;
using HttpClientService;
using WebApi.Dto.Mailer;
using Backend.Core.Exceptions;
using Backend.Core.Extensions;
using Backend.Shared.Services;
using Backend.Shared.Resources;
using HttpClientService.Models;
using Newtonsoft.Json;

public class EmailSenderService : IEmailSenderService
{
    private readonly IHttpClientService _httpClientService;

    private readonly IApplicationSettings _applicationSettings;

    public EmailSenderService(IHttpClientService httpClientService, IApplicationSettings applicationSettings)
    {
        _httpClientService = httpClientService;
        _applicationSettings = applicationSettings;
    }

    public async Task SendNotification(IConfiguration configuration, CancellationToken cancellationToken = default)
    {
        VerifyArguments(configuration);

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

        var baseUrl = _applicationSettings.AzureStorage.BaseUrl;
        var registerForm = _applicationSettings.ApplicationPaths.Templates.RegisterForm;
        var resetPassword = _applicationSettings.ApplicationPaths.Templates.ResetPassword;
        var templateUrl = configuration switch
        {
            CreateUserConfiguration =>  $"{baseUrl}{registerForm}",
            ResetPasswordConfiguration => $"{baseUrl}{resetPassword}",
            _ => ""
        };

        var deploymentOrigin = _applicationSettings.ApplicationPaths.DeploymentOrigin;
        var activationPath = _applicationSettings.ApplicationPaths.ActivationPath;
        var passwordPath = _applicationSettings.ApplicationPaths.UpdatePasswordPath;
        var templateValues = configuration switch
        {
            CreateUserConfiguration createConfiguration => new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", $"{deploymentOrigin}{activationPath}{createConfiguration.ActivationId}" },
                { "{EXPIRATION}", $"{createConfiguration.ActivationIdEnds}" }
            },
            ResetPasswordConfiguration resetConfiguration => new Dictionary<string, string>
            {
                { "{RESET_LINK}", $"{deploymentOrigin}{passwordPath}{resetConfiguration.ResetId}" },
                { "{EXPIRATION}", $"{resetConfiguration.ExpirationDate}" }
            },
            _ => new Dictionary<string, string>()
        };

        var template = await GetEmailTemplate(templateUrl, cancellationToken);
        var payload = new SenderPayloadDto
        {
            From = _applicationSettings.EmailSender.Addresses.Contact,
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

        var payload = JsonConvert.SerializeObject(content);
        var configuration = new Configuration 
        { 
            Url = _applicationSettings.EmailSender.BaseUrl, 
            Method = "POST", 
            Headers = headers,
            StringContent = new StringContent(payload, Encoding.Default, "application/json") 
        };

        var result = await _httpClientService.Execute(configuration, cancellationToken);
        var responseContent = result.Content != null ? Encoding.ASCII.GetString(result.Content) : string.Empty;

        if (result.StatusCode != HttpStatusCode.OK)
        {
            var response = !string.IsNullOrEmpty(responseContent) ? responseContent : "n/a";
            var message = $"{ErrorCodes.CANNOT_SEND_EMAIL}. Full response: {response}";
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), message);
        }
    }

    private static void VerifyArguments(IConfiguration configuration)
    {
        switch (configuration)
        {
            case CreateUserConfiguration createUserConfiguration:
                VerifyCreateUserConfiguration(createUserConfiguration);
                break;
            case ResetPasswordConfiguration resetPasswordConfiguration:
                VerifyResetPasswordConfiguration(resetPasswordConfiguration);
                break;
        }
    }

    private static void VerifyCreateUserConfiguration(CreateUserConfiguration configuration)
    {
        if (configuration.ActivationId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

        if (string.IsNullOrEmpty(configuration.EmailAddress))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL), ErrorCodes.ARGUMENT_EMPTY_OR_NULL);
    }

    private static void VerifyResetPasswordConfiguration(ResetPasswordConfiguration configuration)
    {
        if (configuration.ResetId == Guid.Empty)
            throw new BusinessException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

        if (string.IsNullOrEmpty(configuration.EmailAddress))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL), ErrorCodes.ARGUMENT_EMPTY_OR_NULL);
    }
}