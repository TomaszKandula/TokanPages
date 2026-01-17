using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Invoicing.Currencies.Queries;

public class GetCurrencyCodesQueryHandler : RequestHandler<GetCurrencyCodesQuery, IList<GetCurrencyCodesQueryResult>>
{
    public GetCurrencyCodesQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<IList<GetCurrencyCodesQueryResult>> Handle(GetCurrencyCodesQuery request, CancellationToken cancellationToken)
    {
        var codes = Enum.GetValues<CurrencyCode>();
        var result = codes
            .Select((currencyCodes, index) => new GetCurrencyCodesQueryResult
            {
                SystemCode = index,
                Currency = currencyCodes.ToString().ToUpper()
            })
            .Where(response => response.SystemCode != 0)
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.Currency.Equals(request.FilterBy, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} currency code(s)");
        return await Task.FromResult(result);
    }
}