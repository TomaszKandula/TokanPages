using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetAuthorizationQueryHandler : RequestHandler<GetAuthorizationQuery, GetAuthorizationQueryResult>
{
    private readonly IUserService _userService;
    
    private readonly IPaymentService _paymentService;
    
    public GetAuthorizationQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IPaymentService paymentService, IUserService userService) : base(databaseContext, loggerService)
    {
        _paymentService = paymentService;
        _userService = userService;
    }

    public override async Task<GetAuthorizationQueryResult> Handle(GetAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var output = await _paymentService.GetAuthorization(cancellationToken);
        
        const string info = "New authorization bearer token has been returned from payment provider. Active user ID:";
        LoggerService.LogInformation($"{info} {user.Id}.");

        return new GetAuthorizationQueryResult
        {
            AccessToken = output.AccessToken,
            TokenType = output.TokenType,
            ExpiresIn = output.ExpiresIn,
            GrantType = output.GrantType
        };
    }
}