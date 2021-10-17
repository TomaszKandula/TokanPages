namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using MediatR;

    public class RemoveUserCommandHandler : TemplateHandler<RemoveUserCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        public RemoveUserCommandHandler(DatabaseContext databaseContext) 
            => _databaseContext = databaseContext;

        public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _databaseContext.Users
                .Where(subscribers => subscribers.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!currentUser.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            _databaseContext.Users.Remove(currentUser.First());
            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}