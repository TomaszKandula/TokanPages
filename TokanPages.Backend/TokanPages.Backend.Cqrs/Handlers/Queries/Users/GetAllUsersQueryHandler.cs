namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;

    public class GetAllUsersQueryHandler : TemplateHandler<GetAllUsersQuery, IEnumerable<GetAllUsersQueryResult>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetAllUsersQueryHandler(DatabaseContext databaseContext) 
            => _databaseContext = databaseContext;

        public override async Task<IEnumerable<GetAllUsersQueryResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _databaseContext.Users
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