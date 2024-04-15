using TokanPages.Backend.Application.Subscriptions.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQueryHandler : RequestHandler<GetSubscriptionPricesQuery, GetSubscriptionPricesQueryResult>
{
    public GetSubscriptionPricesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<GetSubscriptionPricesQueryResult> Handle(GetSubscriptionPricesQuery request, CancellationToken cancellationToken)
    {
        var prices = await DatabaseContext.SubscriptionPricing
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

        return prices is null 
            ? new GetSubscriptionPricesQueryResult() 
            : new GetSubscriptionPricesQueryResult { Prices = prices };
    }
}