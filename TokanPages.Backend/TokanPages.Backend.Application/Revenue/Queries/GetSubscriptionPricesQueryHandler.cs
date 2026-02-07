using TokanPages.Backend.Application.Subscriptions.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Revenue;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQueryHandler : RequestHandler<GetSubscriptionPricesQuery, GetSubscriptionPricesQueryResult>
{
    private readonly IRevenueRepository _revenueRepository;

    public GetSubscriptionPricesQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IRevenueRepository revenueRepository) 
        : base(operationDbContext, loggerService) => _revenueRepository = revenueRepository;

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