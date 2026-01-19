using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleCountCommandHandler : RequestHandler<UpdateArticleCountCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;
    
    private readonly IArticlesRepository _articlesRepository;

    public UpdateArticleCountCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var dateTimeStamp = _dateTimeService.Now;
        var isSuccess = await _articlesRepository.UpdateArticleCount(userId, request.Id, dateTimeStamp,  ipAddress, cancellationToken);

        return !isSuccess ? throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS) : Unit.Value;
    }
}