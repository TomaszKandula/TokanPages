using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetVideoAssetQueryHandler : RangeRequestHandler<GetVideoAssetQuery, Unit>
{
    private readonly AppSettingsModel _appSettings;

    public GetVideoAssetQueryHandler(IOptions<AppSettingsModel> options, IHttpContextAccessor httpContextAccessor, 
        IHttpClientServiceFactory clientServiceFactory, ILoggerService loggerService) 
        : base(httpContextAccessor, clientServiceFactory, loggerService) => _appSettings = options.Value;

    public override async Task<Unit> Handle(GetVideoAssetQuery request, CancellationToken cancellationToken)
    {
        var baseAddress = _appSettings.AzStorageBaseUrl;
        var url = $"{baseAddress}/content/assets/{request.BlobName}";

        var range = HttpContextAccessor.HttpContext?.Request.Headers.Range;
        var response = HttpContextAccessor.HttpContext!.Response;
        var configuration = new HttpClientSettings { Url = url, Method = "GET", Range = range };

        var client = ClientServiceFactory.Create(false, LoggerService);
        await client.ProxyRequest(configuration, response, cancellationToken);

        return Unit.Value;
    }
}