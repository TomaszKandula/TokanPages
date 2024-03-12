using System.Text;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Gateway.Models;
using TokanPages.Gateway.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace TokanPages.Gateway.Services;

/// <summary>
/// 
/// </summary>
public class ProxyMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    private readonly ILoggerService _loggerService;

    private readonly IProxyHttpClient _proxyHttpClient;

    private IOptionsSnapshot<GatewaySettings> _settingsAccessor = null!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDelegate"></param>
    /// <param name="loggerService"></param>
    /// <param name="proxyHttpClient"></param>
    public ProxyMiddleware(RequestDelegate requestDelegate, ILoggerService loggerService, IProxyHttpClient proxyHttpClient)
    {
        _requestDelegate = requestDelegate;
        _loggerService = loggerService;
        _proxyHttpClient = proxyHttpClient;
    }

    private IEnumerable<string> ResponseHeadersExclude => _settingsAccessor.Value?.ResponseHeaders?.Exclude ?? Array.Empty<string>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settingsAccessor"></param>
    public async Task Invoke(HttpContext context, IOptionsSnapshot<GatewaySettings> settingsAccessor)
    {
        _settingsAccessor = settingsAccessor;

        var path = context.Request.Path.ToString().ToLower();
        if (path.Contains("/api/websocket") || path == "/hc" || path == "/")
        {
            await _requestDelegate(context);
            return;
        }

        var route = GetRouteForPath(path);
        if (route is null)
        {
            await ResponseWithRouteNotFound(context, context.Request.Path);
        }
        else
        {
            await TryProxyRequest(context, route);
        }
    }

    private RouteDefinition? GetRouteForPath(string path)
    {
        RouteDefinition? route = null;
        foreach (var item in _settingsAccessor.Value?.Routes!)
        {
            var serviceName = item.ServiceName.ToLower();
            if (!path.Contains(serviceName))
                continue;

            route = item;
            route.Path = path;
            break;
        }

        return route;
    }

    private async Task TryProxyRequest(HttpContext context, RouteDefinition route)
    {
        try
        {
            var httpRequestMessage = RequestMessageFactory.Create(context.Request, route, _settingsAccessor.Value.Headers);
            await _proxyHttpClient.ProxyRequest(httpRequestMessage, context, ResponseHeadersExclude);
        }
        catch (Exception exception)
        {
            _loggerService.LogError("Error trying request API:");
            _loggerService.LogError(exception.Message);
            if (!string.IsNullOrWhiteSpace(exception.InnerException?.Message)) _loggerService.LogError(exception.InnerException.Message);
            if (!string.IsNullOrWhiteSpace(exception.StackTrace)) _loggerService.LogError(exception.StackTrace);
            await ResponseWithError(context, context.Request.Path);
        }
    }

    private async Task ResponseWithError(HttpContext context, string path)
    {
        var message = $"Invoking {path} failed";
        _loggerService.LogError(message);
        context.Response.StatusCode = 500;
        await AddMessageToResponse(context, message);
    }

    private async Task ResponseWithRouteNotFound(HttpContext context, string path)
    {
        var message = $"Route {path} not found";
        _loggerService.LogError(message);
        context.Response.StatusCode = 404;
        await AddMessageToResponse(context, message);
    }

    private static async Task AddMessageToResponse(HttpContext context, string message)
    {
        var messageStream = new MemoryStream(Encoding.UTF8.GetBytes(message));
        await messageStream.CopyToAsync(context.Response.Body);
    }
}