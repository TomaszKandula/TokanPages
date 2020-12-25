using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Database;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    
    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public AddUserCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(AddUserCommand ARequest, CancellationToken ACancellationToken)
        {
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
            await FDatabaseContext.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);

        }

    }

}
