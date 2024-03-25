using TokanPages.Gateway.Models;

namespace TokanPages.Gateway.Services.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IProxyRouteProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    RouteDefinition? FindRouteFor(string path);
}