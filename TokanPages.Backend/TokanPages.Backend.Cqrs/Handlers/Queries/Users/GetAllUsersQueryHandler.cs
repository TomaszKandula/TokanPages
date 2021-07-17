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
        private readonly DatabaseContext FDatabaseContext;

        public GetAllUsersQueryHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<IEnumerable<GetAllUsersQueryResult>> Handle(GetAllUsersQuery ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .AsNoTracking()
                .Select(AFields => new GetAllUsersQueryResult 
                { 
                    Id = AFields.Id,
                    AliasName = AFields.UserAlias,
                    Email = AFields.EmailAddress,
                    IsActivated = AFields.IsActivated
                })
                .ToListAsync(ACancellationToken);
            
            return LUsers;
        }
    }
}