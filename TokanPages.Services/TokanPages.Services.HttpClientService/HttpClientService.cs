using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.HttpClientService.Abstractions;
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

    public async Task ProxyRequest(Configuration configuration, HttpResponse response, CancellationToken cancellationToken = default)
    {
        var preparedRequest = PrepareProxyRequest(configuration);
        await ExecuteRangeRequest(response, preparedRequest, cancellationToken);
    }

    public async Task<ExecutionResult> Execute(Configuration configuration, CancellationToken cancellationToken = default)
    {
        VerifyConfigurationArgument(configuration);

        var response = await GetResponse(configuration, cancellationToken);
        var contentType = response.Content.Headers.ContentType;
        var byteContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Redirect)
            return new ExecutionResult
            {
                StatusCode = response.StatusCode,
                ContentType = contentType,
                Content = byteContent
            };

        var stringContent = Encoding.ASCII.GetString(byteContent);
        var content = string.IsNullOrWhiteSpace(stringContent) ? "n/a" : stringContent;
        _loggerService.LogError($"{ErrorCodes.HTTP_REQUEST_FAILED}. Url: '{configuration.Url}'. Response: {content}.");
        throw new BusinessException(nameof(ErrorCodes.HTTP_REQUEST_FAILED), ErrorCodes.HTTP_REQUEST_FAILED);
    }

    public async Task<T> Execute<T>(Configuration configuration, CancellationToken cancellationToken = default)
    {
        VerifyConfigurationArgument(configuration);

        var response = await GetResponse(configuration, cancellationToken);
        var byteContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var stringContent = Encoding.ASCII.GetString(byteContent);
        var content = string.IsNullOrWhiteSpace(stringContent) ? "n/a" : stringContent;

        try
        {
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Redirect)
                return JsonConvert.DeserializeObject<T>(stringContent, GetSettings())!;
        }
        catch (Exception exception)
        {
            _loggerService.LogError($"{ErrorCodes.CANNOT_PARSE}. Exception: {exception}. Url: '{configuration.Url}'. Response: {content}.");
            throw new BusinessException(nameof(ErrorCodes.CANNOT_PARSE), ErrorCodes.CANNOT_PARSE);
        }

        _loggerService.LogError($"{ErrorCodes.HTTP_REQUEST_FAILED}. Url: '{configuration.Url}'. Response: {content}.");
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
        if (configuration.FileData.Length > 0)
        {
            using var formData = new MultipartFormDataContent();
            var content = new StreamContent(new MemoryStream(configuration.FileData));
            formData.Add(content, configuration.FieldName, configuration.FileName);
            return await _httpClient.PostAsync(requestUri, formData, cancellationToken);
        }

        if (configuration.QueryParameters is not null && configuration.QueryParameters.Any())
            requestUri = QueryHelpers.AddQueryString(configuration.Url, configuration.QueryParameters);

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
            },
            NullValueHandling = NullValueHandling.Ignore
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

    private static HttpRequestMessage PrepareProxyRequest(Configuration configuration)
    {
        var url = configuration.Url;
        if (configuration.QueryParameters is not null && configuration.QueryParameters.Any())
            url = QueryHelpers.AddQueryString(url, configuration.QueryParameters);

        var request = new HttpRequestMessage
        {
            Method = new HttpMethod(configuration.Method),
            RequestUri = new Uri(url, UriKind.RelativeOrAbsolute),
        };

        if (configuration.Range is not null && configuration.Range.Value.Count != 0)
            request.Headers.Range = RangeHeaderValue.Parse(configuration.Range);

        if (configuration.Authentication != null)
            ApplyAuthentication(request, configuration.Authentication);

        return request;
    }

    private async Task ExecuteRangeRequest(HttpResponse response, HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var origin = await _httpClient.SendAsync(request, cancellationToken);
        response.StatusCode = (int)origin.StatusCode;

        foreach (var header in origin.Headers)
        {
            response.Headers[header.Key] = header.Value.ToArray();
        }

        foreach (var header in origin.Content.Headers)
        {
            response.Headers[header.Key] = header.Value.ToArray();
        }

        response.Headers.Remove("server");
        response.Headers.Remove("transfer-encoding");

        await origin.Content.CopyToAsync(response.Body, cancellationToken);
    }
}