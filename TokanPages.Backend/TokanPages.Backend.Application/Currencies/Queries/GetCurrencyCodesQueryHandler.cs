using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Currencies.Queries;

public class GetCurrencyCodesQueryHandler : RequestHandler<GetCurrencyCodesQuery, IList<GetCurrencyCodesQueryResult>>
{
    public GetCurrencyCodesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<IList<GetCurrencyCodesQueryResult>> Handle(GetCurrencyCodesQuery request, CancellationToken cancellationToken)
    {
        var codes = Enum.GetValues<CurrencyCodes>();
        var result = codes
            .Select((currencyCodes, index) => new GetCurrencyCodesQueryResult
            {
                SystemCode = index,
                Currency = currencyCodes.ToString().ToUpper()
            })
            .Where(response => response.SystemCode != 0)
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.Currency == request.FilterBy.ToUpper())
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} currency code(s)");
        return await Task.FromResult(result);
    }
}