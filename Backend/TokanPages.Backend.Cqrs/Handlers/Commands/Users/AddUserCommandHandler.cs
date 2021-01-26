using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{   
    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;

        public AddUserCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Guid> Handle(AddUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LEmailCollection = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (LEmailCollection.Count == 1) 
            {
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);
            }

            var LNewId = Guid.NewGuid();
            var LNewUser = new Domain.Entities.Users
            { 
                Id = LNewId,
                EmailAddress = ARequest.EmailAddress,
                IsActivated = true,
                UserAlias = ARequest.UserAlias,
                FirstName = ARequest.FirstName,
                LastName = ARequest.LastName,
                Registered = DateTime.UtcNow,
                LastUpdated = null,
                LastLogged = null
            };

            FDatabaseContext.Users.Add(LNewUser);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(LNewId);
        }
    }
}
