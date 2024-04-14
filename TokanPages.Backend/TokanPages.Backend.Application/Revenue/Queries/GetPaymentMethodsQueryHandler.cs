using TokanPages.Backend.Application.Revenue.Models.Sections;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetPaymentMethodsQueryHandler : RequestHandler<GetPaymentMethodsQuery, GetPaymentMethodsQueryResults>
{
    private readonly IUserService _userService;
    
    private readonly IPaymentService _paymentService;

    public GetPaymentMethodsQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IPaymentService paymentService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _paymentService = paymentService;
    }

    public override async Task<GetPaymentMethodsQueryResults> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var output = await _paymentService.GetPaymentMethods(cancellationToken);

        const string info = "Available payment methods has been returned from payment provider. Active user ID:";
        LoggerService.LogInformation($"{info} {user.Id}.");

        return new GetPaymentMethodsQueryResults
        {
            CardTokens = output.CardTokens?.Select(tokens => new CardTokens
            {
                CardExpirationYear = tokens.CardExpirationYear,
                CardExpirationMonth = tokens.CardExpirationMonth,
                CardNumberMasked = tokens.CardNumberMasked,
                CardScheme = tokens.CardScheme,
                Value = tokens.Value,
                BrandImageUrl = tokens.BrandImageUrl,
                Preferred = tokens.Preferred,
                Status = tokens.Status
            }),
            PexTokens = output.PexTokens?.Select(tokens => new PexTokens
            {
                AccountNumber = tokens.AccountNumber,
                PayType = tokens.PayType,
                Value = tokens.Value,
                Name = tokens.Name,
                BrandImageUrl = tokens.BrandImageUrl,
                Preferred = tokens.Preferred,
                Status = tokens.Status
            }),
            PayByLinks = output.PayByLinks?.Select(links => new PayByLinks
            {
                Value = links.Value,
                BrandImageUrl = links.BrandImageUrl,
                Name = links.Name,
                Status = links.Status,
                MinAmount = links.MaxAmount,
                MaxAmount = links.MaxAmount,
            }),
            Status = new Status
            {
                StatusCode = output.Status?.StatusCode,
                StatusDesc = output.Status?.StatusDesc
            }
        };
    }
}