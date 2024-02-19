using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetVideoAssetQueryHandler : RangeRequestHandler<GetVideoAssetQuery, Unit>
{
    private readonly IConfiguration _configuration;

    public GetVideoAssetQueryHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, 
        IHttpClientServiceFactory clientServiceFactory, ILoggerService loggerService) 
        : base(httpContextAccessor, clientServiceFactory, loggerService) => _configuration = configuration;

    public override async Task<Unit> Handle(GetVideoAssetQuery request, CancellationToken cancellationToken)
    {
        var baseAddress = _configuration.GetValue<string>("AZ_Storage_BaseUrl");
        var url = baseAddress + "/content/assets/" + request.BlobName;

        var range = HttpContextAccessor.HttpContext?.Request.Headers.Range;
        var response = HttpContextAccessor.HttpContext!.Response;
        var configuration = new Configuration { Url = url, Method = "GET", Range = range };

        var client = ClientServiceFactory.Create(false, LoggerService);
        await client.ProxyRequest(configuration, response, cancellationToken);

        return Unit.Value;
    }
}