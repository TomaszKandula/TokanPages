using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.PayUService.Models;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;

namespace TokanPages.Services.PayUService;

[ExcludeFromCodeCoverage]
internal sealed class PayUService : IPayUService
{
    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly ILoggerService _loggerService;

    private readonly AppSettingsModel _appSettings;

    public PayUService(IHttpClientServiceFactory httpClientServiceFactory, 
        ILoggerService loggerService, IOptions<AppSettingsModel> options)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _loggerService = loggerService;
        _appSettings = options.Value;
    }

    public async Task<AuthorizationOutput> GetAuthorization(CancellationToken cancellationToken = default)
    {
        var clientId = _appSettings.PmtClientId;
        var clientSecret = _appSettings.PmtClientSecret;

        var baseUrl = _appSettings.PmtBaseUrl;
        var authorizeUrl = $"{baseUrl}{_appSettings.PmtAddressAuthorize}";

        var payload = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };

        var content = new ContentDictionary { Payload = payload };
        var configuration = new HttpClientSettings
        {
            Url = authorizeUrl,
            Method = "POST",
            PayloadContent = content
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<AuthorizationOutput>(configuration, cancellationToken);
    }

    public async Task<PaymentMethodsOutput> GetPaymentMethods(CancellationToken cancellationToken = default)
    {
        var authorization = await GetAuthorization(cancellationToken);
        var baseUrl = _appSettings.PmtBaseUrl;
        var payMethodsUrl = $"{baseUrl}{_appSettings.PmtAddressPayMethods}";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new HttpClientSettings
        {
            Url = payMethodsUrl,
            Authentication = authentication,
            Method = "GET"
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<PaymentMethodsOutput>(configuration, cancellationToken);
    }

    public async Task<OrderDetailsOutput> GetOrderDetails(string orderId, CancellationToken cancellationToken = default)
    {
        var authorization = await GetAuthorization(cancellationToken);
        var baseUrl = _appSettings.PmtBaseUrl;
        var orderUrl = $"{baseUrl}{_appSettings.PmtAddressOrders}/{orderId}";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new HttpClientSettings
        {
            Url = orderUrl,
            Authentication = authentication,
            Method = "GET"
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<OrderDetailsOutput>(configuration, cancellationToken);
    }

    public async Task<OrderTransactionsOutput> GetOrderTransactions(string orderId, CancellationToken cancellationToken = default)
    {
        var authorization = await GetAuthorization(cancellationToken);
        var baseUrl = _appSettings.PmtBaseUrl;
        var orderUrl = $"{baseUrl}{_appSettings.PmtAddressOrders}/{orderId}/transactions";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new HttpClientSettings
        {
            Url = orderUrl,
            Authentication = authentication,
            Method = "GET"
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<OrderTransactionsOutput>(configuration, cancellationToken);
    }

    public async Task<GenerateCardTokenOutput> GenerateCardToken(GenerateCardTokenInput input, CancellationToken cancellationToken = default)
    {
        var baseUrl = _appSettings.PmtBaseUrl;
        var orderUrl = $"{baseUrl}{_appSettings.PmtAddressTokens}";
        var configuration = new HttpClientSettings
        {
            Url = orderUrl,
            Method = "POST",
            PayloadContent = new ContentString { Payload = input }
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<GenerateCardTokenOutput>(configuration, cancellationToken);
    }

    public async Task<PostOrderOutput> PostOrder(PostOrderInput input, CancellationToken cancellationToken = default)
    {
        var output = await PreparePostOrder(input, cancellationToken);
        var orderUrl = output.OrderUrl;
        var content = output.Content;
        var authentication = output.Authentication;

        var configuration = new HttpClientSettings
        {
            Url = orderUrl ?? string.Empty,
            Authentication = authentication,
            Method = "POST",
            PayloadContent = content
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        return await client.Execute<PostOrderOutput>(configuration, cancellationToken);
    }

    public async Task<string> PostOrderDefault(PostOrderInput input, CancellationToken cancellationToken = default)
    {
        var output = await PreparePostOrder(input, cancellationToken);
        var orderUrl = output.OrderUrl;
        var content = output.Content;
        var authentication = output.Authentication;

        var configuration = new HttpClientSettings
        {
            Url = orderUrl ?? string.Empty,
            Authentication = authentication,
            Method = "POST",
            PayloadContent = content
        };

        var client = _httpClientServiceFactory.Create(false, _loggerService);
        var result = await client.Execute(configuration, cancellationToken);
        var isSuccessful = result.StatusCode is HttpStatusCode.OK or HttpStatusCode.Redirect;
        var hasContent = result.Content is { Length: > 0 };

        if (isSuccessful && hasContent)
            return Encoding.ASCII.GetString(result.Content!); 

        throw new BusinessException(nameof(ErrorCodes.HTTP_REQUEST_FAILED), $"Received status code: {result.StatusCode}.");
    }

    private async Task<PreparePostOrderOutput> PreparePostOrder(PostOrderInput input, CancellationToken cancellationToken = default)
    {
        var authorization = await GetAuthorization(cancellationToken);
        var baseUrl = _appSettings.PmtBaseUrl;
        var orderUrl = $"{baseUrl}{_appSettings.PmtAddressOrders}";

        var content = new ContentString { Payload = input };
        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        return new PreparePostOrderOutput
        {
            OrderUrl = orderUrl,
            Content = content,
            Authentication = authentication
        };
    }
}