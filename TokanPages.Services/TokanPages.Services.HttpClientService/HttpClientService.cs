namespace TokanPages.Services.HttpClientService;

using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Models;
using Authentication;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;

public class HttpClientService : IHttpClientService
{
    private const string Header = "Authorization";

    private const string Basic = "Basic";

    private const string Bearer = "Bearer";

    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient) => _httpClient = httpClient;

    public virtual async Task<ExecutionResult> Execute(Configuration configuration, CancellationToken cancellationToken = default)
    {
        VerifyConfigurationArgument(configuration);

        var requestUri = configuration.Url;
        if (configuration.QueryParameters != null && configuration.QueryParameters.Any())
            requestUri = QueryHelpers.AddQueryString(configuration.Url, configuration.QueryParameters);

        using var request = new HttpRequestMessage(new HttpMethod(configuration.Method), requestUri);

        if (configuration.Headers != null)
            foreach (var (name, value) in configuration.Headers)
            {
                request.Headers.TryAddWithoutValidation(name, value);
            }

        if (configuration.StringContent != null)
            request.Content = configuration.StringContent;

        if (configuration.Authentication != null)
            ApplyAuthentication(request, configuration.Authentication);

        var response = await _httpClient.SendAsync(request, cancellationToken);
        var contentType = response.Content.Headers.ContentType;
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        return new ExecutionResult
        {
            StatusCode = response.StatusCode,
            ContentType = contentType,
            Content = content
        };
    }

    private static string SetAuthentication(string login, string password)
    {
        if (string.IsNullOrEmpty(login))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(login)}' cannot be null or empty.");

        var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));
        return $"{Basic} {base64}";
    }

    private static string SetAuthentication(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(token)}' cannot be null or empty.");

        return $"{Bearer} {token}";
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

    private static void VerifyConfigurationArgument(Configuration configuration)
    {
        if (string.IsNullOrEmpty(configuration.Method))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(configuration.Method)}' cannot be null or empty.");

        if (string.IsNullOrEmpty(configuration.Url))
            throw new BusinessException(nameof(ErrorCodes.ARGUMENT_EMPTY_OR_NULL),
                $"Argument '{nameof(configuration.Url)}' cannot be null or empty.");
    }
}