using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Sender;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewsletterQueryHandler : RequestHandler<GetNewsletterQuery, GetNewsletterQueryResult>
{
    private readonly ISenderRepository _senderRepository;

    public GetNewsletterQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, ISenderRepository senderRepository) 
        : base(operationDbContext, loggerService) => _senderRepository = senderRepository;

    public override async Task<GetNewsletterQueryResult> Handle(GetNewsletterQuery request, CancellationToken cancellationToken) 
    {
        var data = await _senderRepository.GetNewsletter(request.Id);
        if (data == null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        var result = new GetNewsletterQueryResult
        {
            Id = data.Id,
            Email = data.Email,
            IsActivated = data.IsActivated,
            NewsletterCount = data.Count,
            CreatedAt = data.CreatedAt,
            ModifiedAt = data.ModifiedAt
        };

        return result;
    }
}