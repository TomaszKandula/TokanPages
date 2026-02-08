using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Persistence.Caching;

[ExcludeFromCodeCoverage]
public static class AssemblyConfigurer
{
    public static void AddPersistenceCaching(this IServiceCollection services)
    {
        services.AddScoped<IArticlesCache, ArticlesCache>();
        services.AddScoped<IContentCache, ContentCache>();
        services.AddScoped<ICountriesCache, CountriesCache>();
        services.AddScoped<ICurrenciesCache, CurrenciesCache>();
        services.AddScoped<INewslettersCache, NewslettersCache>();
        services.AddScoped<IPaymentsCache, PaymentsCache>();
        services.AddScoped<ISubscriptionsCache, SubscriptionsCache>();
        services.AddScoped<ITemplatesCache, TemplatesCache>();
        services.AddScoped<IUsersCache, UsersCache>();
    }
}