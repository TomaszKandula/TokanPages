using TokanPages.Backend.Application.Subscriptions.Models;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQueryHandler : RequestHandler<GetSubscriptionPricesQuery, GetSubscriptionPricesQueryResult>
{
    private readonly IRevenueRepository _revenueRepository;

    public GetSubscriptionPricesQueryHandler(ILoggerService loggerService, 
        IRevenueRepository revenueRepository) : base(loggerService) => _revenueRepository = revenueRepository;

    public override async Task<GetSubscriptionPricesQueryResult> Handle(GetSubscriptionPricesQuery request, CancellationToken cancellationToken)
    {
        var prices= await _revenueRepository.GetSubscriptionPrices(request.LanguageIso ?? "ENG");

        var result = new List<PriceItem>();
        foreach (var price in prices)
        {
            var priceItem = new PriceItem
            {
                Term = (int)price.Term,
                Price = price.Price,
                CurrencyIso = price.CurrencyIso,
                LanguageIso = price.LanguageIso
            };

            result.Add(priceItem);
        }

        return new GetSubscriptionPricesQueryResult { Prices = result };
    }
}