namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using MediatR;

    public class UpdateUserCommandHandler : TemplateHandler<UpdateUserCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IDateTimeService _dateTimeService;
        
        public UpdateUserCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService) 
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var usersList = await _databaseContext.Users
                .Where(users => users.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!usersList.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var emailCollection = await _databaseContext.Users
                .AsNoTracking()
                .Where(users => users.EmailAddress == request.EmailAddress)
                .ToListAsync(cancellationToken);

            if (emailCollection.Count == 1)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var currentUser = usersList.First();

            currentUser.EmailAddress = request.EmailAddress ?? currentUser.EmailAddress;
            currentUser.UserAlias = request.UserAlias ?? currentUser.UserAlias;
            currentUser.FirstName = request.FirstName ?? currentUser.FirstName;
            currentUser.LastName = request.LastName ?? currentUser.LastName;
            currentUser.IsActivated = request.IsActivated;
            currentUser.LastUpdated = _dateTimeService.Now;

            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}