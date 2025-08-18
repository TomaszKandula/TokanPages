namespace TokanPages.Services.CookieAccessorService;

public interface ICookieAccessor
{
    string? Get(string key);

    void Set(string key, string value, bool httpOnly = true, TimeSpan? maxAge = null);

    void Remove(string key);
}