using TokanPages.Backend.Application.Subscriptions.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQueryHandler : RequestHandler<GetSubscriptionPricesQuery, GetSubscriptionPricesQueryResult>
{
    public GetSubscriptionPricesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<GetSubscriptionPricesQueryResult> Handle(GetSubscriptionPricesQuery request, CancellationToken cancellationToken)
    {
        var prices = await DatabaseContext.SubscriptionsPricing
            .AsNoTracking()
            .Where(pricing => pricing.LanguageIso == request.LanguageIso)
            .Select(pricing => new PriceItem
            {
                Term = (int)pricing.Term,
                Price = pricing.Price,
                CurrencyIso = pricing.CurrencyIso,
                LanguageIso = pricing.LanguageIso
            })
            .ToListAsync(cancellationToken);

        return new GetSubscriptionPricesQueryResult { Prices = prices };
    }
}