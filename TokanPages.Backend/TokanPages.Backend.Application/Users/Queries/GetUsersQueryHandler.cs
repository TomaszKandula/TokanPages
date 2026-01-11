using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUsersQueryHandler : RequestHandler<GetUsersQuery, List<GetUsersQueryResult>>
{
    public GetUsersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetUsersQueryResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await DatabaseContext.Users
            .AsNoTracking()
            .Select(user => new GetUsersQueryResult 
            { 
                Id = user.Id,
                AliasName = user.UserAlias,
                Email = user.EmailAddress,
                IsActivated = user.IsActivated
            })
            .ToListAsync(cancellationToken);
    }
}