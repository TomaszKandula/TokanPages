using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Sender;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class AddNewsletterCommandHandler : RequestHandler<AddNewsletterCommand, Guid>
{
    private readonly ISenderRepository _senderRepository;

    public AddNewsletterCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        ISenderRepository senderRepository) : base(operationDbContext, loggerService) => _senderRepository = senderRepository;

    public override async Task<Guid> Handle(AddNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var data = await _senderRepository.GetNewsletter(request.Email);
        if (data != null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var id = Guid.NewGuid();
        await _senderRepository.CreateNewsletter(request.Email, id);

        return id;
    }
}