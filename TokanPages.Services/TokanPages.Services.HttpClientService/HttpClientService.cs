using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Authentication;
using TokanPages.Services.HttpClientService.Models;

namespace TokanPages.Services.HttpClientService;

public class HttpClientService : IHttpClientService
{
    private const string Header = "Authorization";

    private const string Basic = "Basic";

    private const string Bearer = "Bearer";

    private readonly HttpClient _httpClient;

    private readonly ILoggerService _loggerService;

    public HttpClientService(HttpClient httpClient, ILoggerService loggerService)
    {
        _httpClient = httpClient;
        _loggerService = loggerService;
    }

    public async Task<ExecutionResult> Execute(Configuration configuration, CancellationToken cancellationToken = default)
    {
        VerifyConfigurationArgument(configuration);

        var response = await GetResponse(configuration, cancellationToken);
        var contentType = response.Content.Headers.ContentType;
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
            return new ExecutionResult
            {
                StatusCode = response.StatusCode,
                ContentType = contentType,
                Content = content
            };

        var stringContent = Encoding.ASCII.GetString(content);
        _loggerService.LogError($"{ErrorCodes.HTTP_REQUEST_FAILED}. Full response: {stringContent}.");
        throw new BusinessException(nameof(ErrorCodes.HTTP_REQUEST_FAILED), ErrorCodes.HTTP_REQUEST_FAILED);
    }

    public async Task<T> Execute<T>(Configuration configuration, CancellationToken cancellationToken = default)
    {
        VerifyConfigurationArgument(configuration);

        var response = await GetResponse(configuration, cancellationToken);
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var stringContent = Encoding.ASCII.GetString(content);

        if (response.IsSuccessStatusCode) 
            return JsonConvert.DeserializeObject<T>(stringContent, GetSettings())!;

        _loggerService.LogError($"{ErrorCodes.HTTP_REQUEST_FAILED}. Full response: {stringContent}.");
        throw new BusinessException(nameof(ErrorCodes.HTTP_REQUEST_FAILED), ErrorCodes.HTTP_REQUEST_FAILED);
    }

    private static HttpContent PrepareContent(IPayloadContent content)
    {
        switch (content)
        {
            case ContentDictionary contentDictionary:
                return new FormUrlEncodedContent(contentDictionary.Payload);

            case ContentString contentString:
                var serialized = JsonConvert.SerializeObject(contentString.Payload, GetSettings());
                return new StringContent(serialized, Encoding.Default, "application/json");

            default: 
                throw new BusinessException("Unsupported content type."); 
        }
    }

    private async Task<HttpResponseMessage> GetResponse(Configuration configuration, CancellationToken cancellationToken = default)
    {
        var requestUri = configuration.Url;
        if (configuration.QueryParameters is not null && configuration.QueryParameters.Any())
            requestUri = QueryHelpers.AddQueryString(configuration.Url, configuration.QueryParameters!);

        using var request = new HttpRequestMessage(new HttpMethod(configuration.Method), requestUri);

        if (configuration.Headers != null)
        {
            foreach (var (name, value) in configuration.Headers)
            {
                request.Headers.TryAddWithoutValidation(name, value);
            }
        }

        if (configuration.PayloadContent is not null)
            request.Content = PrepareContent(configuration.PayloadContent);

        if (configuration.Authentication != null)
            ApplyAuthentication(request, configuration.Authentication);

        return await _httpClient.SendAsync(request, cancellationToken);
    }

    private static JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }

    private static void ApplyAuthentication(HttpRequestMessage request, IAuthentication authentication)
    {
        switch (authentication)
        {
            case BasicAuthentication basicAuthentication:
                var basic = SetAuthentication(basicAuthentication.Login, basicAuthentication.Password);
                request.Headers.TryAddWithoutValidation(Header, basic); 
                break;
                
            case BearerAuthentication bearerAuthentication:
                var bearer = SetAuthentication(bearerAuthentication.Token);
                request.Headers.TryAddWithoutValidation(Header, bearer); 
                break;
        }
    }

    private static string SetAuthentication(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
        {
            const string message = $"Argument '{nameof(login)}' cannot be null or empty.";
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),message);
        }

        var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));
        return $"{Basic} {base64}";
    }

    private static string SetAuthentication(string token)
    {
        if (!string.IsNullOrEmpty(token)) return $"{Bearer} {token}";

        const string message = $"Argument '{nameof(token)}' cannot be null or empty.";
        throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL), message);
    }

    private static void VerifyConfigurationArgument(Configuration configuration)
    {
        string message;

        if (string.IsNullOrEmpty(configuration.Method))
        {
            message = $"Argument '{nameof(configuration.Method)}' cannot be null or empty.";
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL), message);
        }

        if (!string.IsNullOrEmpty(configuration.Url)) return;

        message = $"Argument '{nameof(configuration.Url)}' cannot be null or empty.";
        throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL), message);
    }
}