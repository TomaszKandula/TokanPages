using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    public class RemoveUserCommandHandler : TemplateHandler<RemoveUserCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        public RemoveUserCommandHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<Unit> Handle(RemoveUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LCurrentUser = await FDatabaseContext.Users
                .Where(ASubscribers => ASubscribers.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LCurrentUser.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            FDatabaseContext.Users.Remove(LCurrentUser.First());
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}
