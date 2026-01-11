using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Invoicing.Countries.Queries;

public class GetCountryCodesQueryHandler : RequestHandler<GetCountryCodesQuery, IList<GetCountryCodesQueryResult>>
{
    public GetCountryCodesQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService)
        : base(operationsDbContext, loggerService) { }

    public override async Task<IList<GetCountryCodesQueryResult>> Handle(GetCountryCodesQuery request, CancellationToken cancellationToken)
    {
        var codes = Enum.GetValues<CountryCode>();
        var result = codes
            .Select((countryCodes, index) => new GetCountryCodesQueryResult
            {
                SystemCode = index,
                Country = countryCodes.ToString().ToUpper()
            })
            .Where(response => response.SystemCode != 0)
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.Country.Equals(request.FilterBy, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} country code(s)");
        return await Task.FromResult(result);
    }
}