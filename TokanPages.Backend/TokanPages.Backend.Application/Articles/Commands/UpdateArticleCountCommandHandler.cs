using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleCountCommandHandler : RequestHandler<UpdateArticleCountCommand, Unit>
{
    private readonly IUserService _userService;
    
    private readonly IArticlesRepository _articlesRepository;

    public UpdateArticleCountCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();

        var articleCount = (await _articlesRepository.GetArticleCount(ipAddress, request.Id)).SingleOrDefault();
        if (articleCount is not null)
        {
            var readCount = articleCount.ReadCount + 1;
            await _articlesRepository.UpdateArticleCount(userId, request.Id, readCount, ipAddress);
        }
        else
        {
            await _articlesRepository.CreateArticleCount(userId, request.Id, ipAddress);
        }

        return Unit.Value;
    }
}