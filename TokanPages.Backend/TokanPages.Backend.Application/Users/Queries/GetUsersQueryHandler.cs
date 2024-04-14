using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUsersQueryHandler : RequestHandler<GetUsersQuery, List<GetUsersQueryResult>>
{
    public GetUsersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetUsersQueryResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await DatabaseContext.Users
            .AsNoTracking()
            .Select(users => new GetUsersQueryResult 
            { 
                Id = users.Id,
                AliasName = users.UserAlias,
                Email = users.EmailAddress,
                IsActivated = users.IsActivated
            })
            .ToListAsync(cancellationToken);
    }
}