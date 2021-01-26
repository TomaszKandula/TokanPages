using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users
{
    public class GetUserQueryHandler : TemplateHandler<GetUserQuery, Domain.Entities.Users>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetUserQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Domain.Entities.Users> Handle(GetUserQuery ARequest, CancellationToken ACancellationToken)
        {
            var LCurrentUser = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUser => AUser.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LCurrentUser.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);
            }

            return LCurrentUser.First();
        }
    }
}
