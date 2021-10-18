namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MediatR;
    using Database;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    
    public class ActivateUserCommandHandler : TemplateHandler<ActivateUserCommand, Unit>
    {
        private readonly IDateTimeService _dateTimeService;
       
        public ActivateUserCommandHandler(DatabaseContext databaseContext, ILogger logger, 
            IDateTimeService dateTimeService) : base(databaseContext, logger)
        {
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var users = await DatabaseContext.Users
                .SingleOrDefaultAsync(users => users.ActivationId == request.ActivationId, cancellationToken);

            if (users == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

            if (users.ActivationIdEnds < _dateTimeService.Now)
                throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);
            
            users.IsActivated = true;
            users.ActivationId = null;
            users.ActivationIdEnds = null;

            Logger.LogInformation($"User account has been activated, user ID: {users.Id}");
            await DatabaseContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}