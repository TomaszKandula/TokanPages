using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryHandler : RequestHandler<GetUserQuery, GetUserQueryResult>
{
    public GetUserQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) : base(operationDbContext, loggerService) { }

    public override async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var query = await (from user in OperationDbContext.Users
            join userInfo in OperationDbContext.UserInformation
            on user.Id equals userInfo.UserId
            where user.Id == request.Id
            select new GetUserQueryResult
            {
                Id = user.Id,
                Email = user.EmailAddress,
                AliasName = user.UserAlias,
                IsActivated = user.IsActivated,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Registered = user.CreatedAt,
                LastUpdated = userInfo.ModifiedAt
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        return query ?? throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);
    }
}