using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Invoicing.Payments.Queries;

public class GetPaymentTypeQueryHandler : RequestHandler<GetPaymentTypeQuery, IList<GetPaymentTypeQueryResult>>
{
    public GetPaymentTypeQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService) 
        : base(operationsDbContext, loggerService) { }

    public override async Task<IList<GetPaymentTypeQueryResult>> Handle(GetPaymentTypeQuery request, CancellationToken cancellationToken)
    {
        var types = Enum.GetValues<PaymentType>();
        var result = types
            .Select((paymentTypes, index) => new GetPaymentTypeQueryResult
            {
                SystemCode = index,
                PaymentType = paymentTypes.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrWhiteSpace(request.FilterBy), 
                response => response.PaymentType.Equals(request.FilterBy, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} payment type(s)");
        return await Task.FromResult(result);
    }
}