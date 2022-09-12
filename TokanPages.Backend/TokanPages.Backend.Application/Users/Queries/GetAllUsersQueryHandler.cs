namespace TokanPages.Backend.Application.Users.Queries;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Utilities.LoggerService;

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