using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Sender;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewslettersQueryHandler : RequestHandler<GetNewslettersQuery, List<GetNewslettersQueryResult>>
{
    private readonly ISenderRepository _senderRepository;

    public GetNewslettersQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, ISenderRepository senderRepository) 
        : base(operationDbContext, loggerService) => _senderRepository = senderRepository;

    public override async Task<List<GetNewslettersQueryResult>> Handle(GetNewslettersQuery request, CancellationToken cancellationToken)
    {
        var data = await _senderRepository.GetNewsletters(true);
        var result = new List<GetNewslettersQueryResult>();

        foreach (var item in data)
        {
            var newsletter = new GetNewslettersQueryResult
            {
                Id = item.Id,
                Email = item.Email,
                IsActivated = item.IsActivated,
                NewsletterCount = item.Count
            };

            result.Add(newsletter);
        }

        return result;
    }
}