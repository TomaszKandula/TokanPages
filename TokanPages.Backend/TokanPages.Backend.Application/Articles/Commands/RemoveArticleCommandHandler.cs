using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    public RemoveArticleCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var userId = _userService.GetLoggedUserId();
        await _articlesRepository.RemoveArticle(userId, request.Id);
        return Unit.Value;
    }
}