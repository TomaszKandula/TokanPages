using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.PayUService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetAuthorizationQueryHandler : RequestHandler<GetAuthorizationQuery, GetAuthorizationQueryResult>
{
    private readonly IUserService _userService;
    
    private readonly IPayUService _payUService;
    
    public GetAuthorizationQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IPayUService payUService, IUserService userService) : base(operationDbContext, loggerService)
    {
        _payUService = payUService;
        _userService = userService;
    }

    public override async Task<GetAuthorizationQueryResult> Handle(GetAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var output = await _payUService.GetAuthorization(cancellationToken);
        
        const string info = "New authorization bearer token has been returned from payment provider. Active user ID:";
        LoggerService.LogInformation($"{info} {userId}.");

        return new GetAuthorizationQueryResult
        {
            AccessToken = output.AccessToken,
            TokenType = output.TokenType,
            ExpiresIn = output.ExpiresIn,
            GrantType = output.GrantType
        };
    }
}