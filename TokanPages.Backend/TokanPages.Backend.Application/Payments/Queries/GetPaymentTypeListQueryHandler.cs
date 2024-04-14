using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Payments.Queries;

public class GetPaymentTypeListQueryHandler : RequestHandler<GetPaymentTypeListQuery, IList<GetPaymentTypeListQueryResult>>
{
    public GetPaymentTypeListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<IList<GetPaymentTypeListQueryResult>> Handle(GetPaymentTypeListQuery request, CancellationToken cancellationToken)
    {
        var types = Enum.GetValues<PaymentTypes>();
        var result = types
            .Select((paymentTypes, index) => new GetPaymentTypeListQueryResult
            {
                SystemCode = index,
                PaymentType = paymentTypes.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.PaymentType == request.FilterBy.ToUpper())
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} payment type(s)");
        return await Task.FromResult(result);
    }
}