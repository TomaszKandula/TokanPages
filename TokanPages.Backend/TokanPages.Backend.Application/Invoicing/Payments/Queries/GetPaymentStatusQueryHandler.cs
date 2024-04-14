using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

public class GetPaymentStatusQueryHandler : RequestHandler<GetPaymentStatusQuery, IList<GetPaymentStatusQueryResult>>
{
    public GetPaymentStatusQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<IList<GetPaymentStatusQueryResult>> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues<PaymentStatuses>();
        var result = statuses
            .Select((paymentStatuses, index) => new GetPaymentStatusQueryResult
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