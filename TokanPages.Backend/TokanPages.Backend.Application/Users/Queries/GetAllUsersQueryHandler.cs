using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetAllUsersQueryHandler : RequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResult>>
{
    public GetAllUsersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllUsersQueryResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await DatabaseContext.Users
            .AsNoTracking()
            .Select(users => new GetAllUsersQueryResult 
            { 
                Id = users.Id,
                AliasName = users.UserAlias,
                Email = users.EmailAddress,
                IsActivated = users.IsActivated
            })
            .ToListAsync(cancellationToken);
    }
}