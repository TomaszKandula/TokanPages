using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Countries.Queries;

public class GetCountryCodesQueryHandler : RequestHandler<GetCountryCodesQuery, IEnumerable<GetCountryCodesQueryResult>>
{
    public GetCountryCodesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService)
        : base(databaseContext, loggerService) { }

    public override async Task<IEnumerable<GetCountryCodesQueryResult>> Handle(GetCountryCodesQuery request, CancellationToken cancellationToken)
    {
        var codes = Enum.GetValues<CountryCodes>();
        var result = codes
            .Select((countryCodes, index) => new GetCountryCodesQueryResult
            {
                SystemCode = index,
                Country = countryCodes.ToString().ToUpper()
            })
            .Where(response => response.SystemCode != 0)
            .WhereIf(
                !string.IsNullOrEmpty(request.FilterBy), 
                response => response.Country == request.FilterBy.ToUpper())
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} country code(s)");
        return await Task.FromResult(result);
    }
}