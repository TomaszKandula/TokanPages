using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetAllUsersQueryHandler : TemplateHandler<GetAllUsersQuery, IEnumerable<Domain.Entities.Users>>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetAllUsersQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<IEnumerable<Domain.Entities.Users>> Handle(GetAllUsersQuery ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users.AsNoTracking().ToListAsync(ACancellationToken);
            return LUsers;
        }
    }
}
