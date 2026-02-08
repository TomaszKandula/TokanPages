using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Sender;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class RemoveNewsletterCommandHandler : RequestHandler<RemoveNewsletterCommand, Unit>
{
    private readonly ISenderRepository _senderRepository;
    
    public RemoveNewsletterCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        ISenderRepository senderRepository) : base(operationDbContext, loggerService) => _senderRepository = senderRepository;

    public override async Task<Unit> Handle(RemoveNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var newsletterData = await _senderRepository.GetNewsletter(request.Id);
        if (newsletterData is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        await _senderRepository.RemoveNewsletter(request.Id);
        return Unit.Value;
    }
}