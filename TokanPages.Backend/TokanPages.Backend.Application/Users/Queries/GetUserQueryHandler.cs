namespace TokanPages.Backend.Application.Handlers.Queries.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;

public class GetUserQueryHandler : RequestHandler<GetUserQuery, GetUserQueryResult>
{
    public GetUserQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var query = await (from users in DatabaseContext.Users
            join userInfo in DatabaseContext.UserInfo
            on users.Id equals userInfo.UserId
            where users.Id == request.Id
            select new GetUserQueryResult
            {
                Id = users.Id,
                Email = users.EmailAddress,
                AliasName = users.UserAlias,
                IsActivated = users.IsActivated,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Registered = users.CreatedAt,
                LastUpdated = userInfo.ModifiedAt
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (query is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return query;
    }
}