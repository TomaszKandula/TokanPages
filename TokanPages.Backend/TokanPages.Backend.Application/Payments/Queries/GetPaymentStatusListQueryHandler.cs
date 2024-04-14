using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Payments.Queries;

public class GetPaymentStatusListQueryHandler : RequestHandler<GetPaymentStatusListQuery, IList<GetPaymentStatusListQueryResult>>
{
    public GetPaymentStatusListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<IList<GetPaymentStatusListQueryResult>> Handle(GetPaymentStatusListQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues<PaymentStatuses>();
        var result = statuses
            .Select((paymentStatuses, index) => new GetPaymentStatusListQueryResult
            {
                SystemCode = index,
                PaymentStatus = paymentStatuses.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.PaymentStatus == request.FilterBy.ToUpper())
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} payment status(es)");
        return await Task.FromResult(result);
    }
}