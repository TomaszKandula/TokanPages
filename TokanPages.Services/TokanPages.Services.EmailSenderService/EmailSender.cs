namespace TokanPages.Services.EmailSenderService;

using System.Net;
using System.Text;
using Models;
using Backend.Shared;
using Models.Interfaces;
using HttpClientService;
using Backend.Shared.Models;
using Backend.Core.Exceptions;
using Backend.Core.Extensions;
using Backend.Shared.Services;
using Backend.Shared.Resources;
using HttpClientService.Models;

public class EmailSender : IEmailSender
{
    private readonly IHttpClientService _httpClientService;

    private readonly IApplicationSettings _applicationSettings;

    public EmailSender(IHttpClientService httpClientService, IApplicationSettings applicationSettings)
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
        
        var templateUrl = configuration switch
        {
            CreateUserConfiguration =>  $"{_applicationSettings.AzureStorage.BaseUrl}{Constants.Emails.Templates.RegisterForm}",
            ResetPasswordConfiguration => $"{_applicationSettings.AzureStorage.BaseUrl}{Constants.Emails.Templates.ResetPassword}",
            _ => ""
        };

        var templateValues = configuration switch
        {
            CreateUserConfiguration createConfiguration => new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.ActivationPath}{createConfiguration.ActivationId}" },
                { "{EXPIRATION}", $"{createConfiguration.ActivationIdEnds}" }
            },
            ResetPasswordConfiguration resetConfiguration => new Dictionary<string, string>
            {
                { "{RESET_LINK}", $"{_applicationSettings.ApplicationPaths.DeploymentOrigin}{_applicationSettings.ApplicationPaths.UpdatePasswordPath}{resetConfiguration.ResetId}" },
                { "{EXPIRATION}", $"{resetConfiguration.ExpirationDate}" }
            },
            _ => new Dictionary<string, string>()
        };
        
        var template = await GetEmailTemplate(templateUrl, cancellationToken);
        var payload = new EmailSenderPayload
        {
            From = Constants.Emails.Addresses.Contact,
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

        var configuration = new Configuration 
        { 
            Url = _applicationSettings.EmailSender.BaseUrl, 
            Method = "POST", 
            Headers = headers,
            StringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(content), Encoding.Default, "application/json") 
        };

        var result = await _httpClientService.Execute(configuration, cancellationToken);
        if (result.StatusCode != HttpStatusCode.OK)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}");
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