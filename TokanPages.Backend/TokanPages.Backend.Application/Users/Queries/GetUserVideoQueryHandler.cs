using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserVideoQueryHandler : RangeRequestHandler<GetUserVideoQuery, Unit>
{
    private readonly IConfiguration _configuration;

    public GetUserVideoQueryHandler(IHttpContextAccessor httpContextAccessor, IHttpClientServiceFactory clientServiceFactory, 
        ILoggerService loggerService,IConfiguration configuration) 
        : base(httpContextAccessor, clientServiceFactory, loggerService) => _configuration = configuration;

    public override async Task<Unit> Handle(GetUserVideoQuery request, CancellationToken cancellationToken)
    {
        var baseAddress = _configuration.GetValue<string>("AZ_Storage_BaseUrl");
        var url = $"{baseAddress}/content/users/{request.Id}/videos/{request.BlobName}";

        var range = HttpContextAccessor.HttpContext?.Request.Headers.Range;
        var response = HttpContextAccessor.HttpContext!.Response;
        var configuration = new Configuration { Url = url, Method = "GET", Range = range };

        var client = ClientServiceFactory.Create(false, LoggerService);
        await client.ProxyRequest(configuration, response, cancellationToken);

        return Unit.Value;
    }
}