using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.DateTimeService;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    public class UpdateUserCommandHandler : TemplateHandler<UpdateUserCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IDateTimeService FDateTimeService;
        
        public UpdateUserCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService) 
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
        }

        public override async Task<Unit> Handle(UpdateUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .Where(AUser => AUser.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LUsers.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var LEmailCollection = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);

            if (LEmailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var LCurrentUser = LUsers.First();

            LCurrentUser.EmailAddress = ARequest.EmailAddress ?? LCurrentUser.EmailAddress;
            LCurrentUser.UserAlias = ARequest.UserAlias ?? LCurrentUser.UserAlias;
            LCurrentUser.FirstName = ARequest.FirstName ?? LCurrentUser.FirstName;
            LCurrentUser.LastName = ARequest.LastName ?? LCurrentUser.LastName;
            LCurrentUser.IsActivated = ARequest.IsActivated;
            LCurrentUser.LastUpdated = FDateTimeService.Now;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}
