using System;
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

    public class UpdateUserCommandHandler : TemplateHandler<UpdateUserCommand, Unit>
    {

        private readonly DatabaseContext FDatabaseContext;

        public UpdateUserCommandHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Unit> Handle(UpdateUserCommand ARequest, CancellationToken ACancellationToken)
        {

            var LCurrentUser = await FDatabaseContext.Users
                .Where(AUser => AUser.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LCurrentUser.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);
            }

            LCurrentUser.First().EmailAddress = ARequest.EmailAddress;
            LCurrentUser.First().UserAlias = ARequest.UserAlias;
            LCurrentUser.First().FirstName = ARequest.FirstName;
            LCurrentUser.First().LastName = ARequest.LastName;
            LCurrentUser.First().IsActivated = ARequest.IsActivated;
            LCurrentUser.First().LastUpdated = DateTime.UtcNow;

            FDatabaseContext.Users.Attach(LCurrentUser.First()).State = EntityState.Modified;
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);

        }

    }

}
