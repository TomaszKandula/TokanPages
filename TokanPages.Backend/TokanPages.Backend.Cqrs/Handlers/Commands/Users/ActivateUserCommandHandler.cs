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
        private readonly DatabaseContext _databaseContext;

        private readonly IDateTimeService _dateTimeService;
        
        private readonly ILogger _logger;
        
        public ActivateUserCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService, ILogger logger)
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        public override async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _databaseContext.Users
                .SingleOrDefaultAsync(users => users.ActivationId == request.ActivationId, cancellationToken);

            if (users == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

            if (users.ActivationIdEnds < _dateTimeService.Now)
                throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);
            
            users.IsActivated = true;
            users.ActivationId = null;
            users.ActivationIdEnds = null;

            _logger.LogInformation($"User account has been activated, user ID: {users.Id}");
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}