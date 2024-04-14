using System.Net;
using System.Text;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.PayUService.Models;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Services.PayUService;

public class PayUService : IPayUService
{
    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly ILoggerService _loggerService;

    private readonly IConfiguration _configuration;

    public PayUService(IHttpClientServiceFactory httpClientServiceFactory, 
        ILoggerService loggerService, IConfiguration configuration)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _loggerService = loggerService;
        _configuration = configuration;
    }

    public async Task<AuthorizationOutput> GetAuthorization(CancellationToken cancellationToken = default)
    {
        var clientId = _configuration.GetValue<string>("Pmt_ClientId");
        var clientSecret = _configuration.GetValue<string>("Pmt_ClientSecret");

        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var authorizeUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_Authorize")}";

        var payload = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };

        var content = new ContentDictionary { Payload = payload };
        var configuration = new Configuration
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
        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var payMethodsUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_PayMethods")}";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new Configuration
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
        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var orderUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_Orders")}/{orderId}";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new Configuration
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
        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var orderUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_Orders")}/{orderId}/transactions";

        var authentication = new BearerAuthentication
        {
            Token = authorization.AccessToken ?? string.Empty
        };

        var configuration = new Configuration
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
        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var orderUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_Tokens")}";
        var configuration = new Configuration
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

        var configuration = new Configuration
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

        var configuration = new Configuration
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
        var baseUrl = _configuration.GetValue<string>("Pmt_BaseUrl");
        var orderUrl = $"{baseUrl}{_configuration.GetValue<string>("Pmt_Address_Orders")}";

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