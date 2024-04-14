using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.HttpClientService.Abstractions;
using TokanPages.Services.HttpClientService.Models;
using TokanPages.Services.MetricsService.Models;

namespace TokanPages.Services.MetricsService;

[ExcludeFromCodeCoverage]
public class MetricsService : IMetricsService
{
    private readonly IHttpClientServiceFactory _httpClientServiceFactory;

    private readonly ILoggerService _loggerService;

    private readonly IConfiguration _configuration;

    public MetricsService(IHttpClientServiceFactory httpClientServiceFactory, ILoggerService loggerService, IConfiguration configuration)
    {
        _httpClientServiceFactory = httpClientServiceFactory;
        _loggerService = loggerService;
        _configuration = configuration;
    }

    public async Task<IActionResult> GetMetrics(string project, string metric)
    {
        var server = _configuration.GetValue<string>("SonarQube_Server");
        var token = await GetProjectToken(project);
        var requestUrl = $"{server}/api/project_badges/measure?project={project}&metric={metric}&token={token}";

        return await GetProjectBadge(requestUrl);
    }

    public async Task<IActionResult> GetQualityGate(string project)
    {
        var server = _configuration.GetValue<string>("SonarQube_Server");
        var token = await GetProjectToken(project);
        var requestUrl = $"{server}/api/project_badges/quality_gate?project={project}&token={token}";

        return await GetProjectBadge(requestUrl);
    }

    private async Task<string> GetProjectToken(string projectName)
    {
        var server = _configuration.GetValue<string>("SonarQube_Server");
        var token = _configuration.GetValue<string>("SonarQube_Token");
        var url = $"{server}/api/project_badges/token?project={projectName}";

        var authentication = new BasicAuthentication
        {
            Login = token,
            Password = string.Empty
        };

        var configuration = new Configuration
        {
            Url = url, 
            Method = "GET",
            Authentication = authentication
        };

        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute<TokenResponse>(configuration);
        return result.Token;
    }

    private async Task<FileContentResult> GetProjectBadge(string requestUrl)
    {
        var configuration = new Configuration
        {
            Url = requestUrl, 
            Method = "GET"
        };

        var client = _httpClientServiceFactory.Create(true, _loggerService);
        var result = await client.Execute(configuration);

        if (result.StatusCode == HttpStatusCode.OK)
            return new FileContentResult(result.Content!, result.ContentType?.MediaType!);

        var message = result.Content is null 
            ? ErrorCodes.ERROR_UNEXPECTED 
            : Encoding.Default.GetString(result.Content);

        _loggerService.LogError($"Received null content with status code: {result.StatusCode}");
        throw new BusinessException(message);
    }
}