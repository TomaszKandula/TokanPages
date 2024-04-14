using TokanPages.Backend.Application.Revenue.Models.Sections;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderTransactionsQueryHandler : RequestHandler<GetOrderTransactionsQuery, GetOrderTransactionsQueryResult>
{
    private readonly IUserService _userService;

    private readonly IPayUService _payUService;

    public GetOrderTransactionsQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IPayUService payUService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _payUService = payUService;
    }

    public override async Task<GetOrderTransactionsQueryResult> Handle(GetOrderTransactionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var output = await _payUService.GetOrderTransactions(request.OrderId, cancellationToken);

        const string info = "Order transactions have been returned from payment provider. Active user ID:";
        LoggerService.LogInformation($"{info} {user.Id}.");

        return new GetOrderTransactionsQueryResult
        {
            Transactions = output.Transactions?.Select(transactions => new Transactions
            {
                PayMethod = new PayMethod
                {
                    Type = transactions.PayMethod?.Type
                },
                PaymentFlow = transactions.PaymentFlow,
                Card = new Card
                {
                    CardData = transactions.Card?.CardData,
                    CardInstallmentProposal = transactions.Card?.CardInstallmentProposal,
                    FirstTransactionId = transactions.Card?.FirstTransactionId
                },
                BankAccount = new BankAccount
                {
                    Number = transactions.BankAccount?.Number,
                    Name = transactions.BankAccount?.Name,
                    City = transactions.BankAccount?.City,
                    PostalCode = transactions.BankAccount?.PostalCode,
                    Street = transactions.BankAccount?.Street,
                    Address = transactions.BankAccount?.Address
                }
            })
        };
    }
}