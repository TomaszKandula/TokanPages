﻿namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Core.Logger;
    using Database;

    public class GetAllUsersQueryHandler : TemplateHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResult>>
    {
        public GetAllUsersQueryHandler(DatabaseContext databaseContext, ILogger logger) : base(databaseContext, logger) { }

        public override async Task<IEnumerable<GetAllUsersQueryResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await DatabaseContext.Users
                .AsNoTracking()
                .Select(users => new GetAllUsersQueryResult 
                { 
                    Id = users.Id,
                    AliasName = users.UserAlias,
                    Email = users.EmailAddress,
                    IsActivated = users.IsActivated
                })
                .ToListAsync(cancellationToken);
            
            return users;
        }
    }
}