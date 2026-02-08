using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

public class GetPaymentStatusQueryHandler : RequestHandler<GetPaymentStatusQuery, IList<GetPaymentStatusQueryResult>>
{
    public GetPaymentStatusQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<IList<GetPaymentStatusQueryResult>> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues<PaymentStatus>();
        var result = statuses
            .Select((paymentStatuses, index) => new GetPaymentStatusQueryResult
            {
                SystemCode = index,
                PaymentStatus = paymentStatuses.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.PaymentStatus.Equals(request.FilterBy, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} payment status(es)");
        return await Task.FromResult(result);
    }
}