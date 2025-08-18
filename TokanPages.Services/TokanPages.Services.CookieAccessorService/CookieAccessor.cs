using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.Services.CookieAccessorService;

public class CookieAccessor : ICookieAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILoggerService _loggerService;

    public CookieAccessor(IHttpContextAccessor httpContextAccessor, ILoggerService loggerService)
    {
        _loggerService = loggerService;
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Get(string key)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            _loggerService.LogError("HttpContext is null");
            return null;
        }

        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(key, out var cookie);
        return cookie;
    }

    public void Set(string key, string value, bool httpOnly = true, TimeSpan? maxAge = null)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            _loggerService.LogError("HttpContext is null");
            return;
        }

        if (_httpContextAccessor.HttpContext.Response.HasStarted)
        {
            _loggerService.LogInformation("HttpContext has already started");
            return;
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, new CookieOptions
        {
            HttpOnly = httpOnly,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            MaxAge = maxAge ?? TimeSpan.FromDays(1),
        });
    }

    public void Remove(string key)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            _loggerService.LogError("HttpContext is null");
            return;
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
    }
}