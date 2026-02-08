using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using TokanPages.Persistence.DataAccess.Repositories.Sender.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class UpdateNewsletterCommandHandler : RequestHandler<UpdateNewsletterCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ISenderRepository _senderRepository;

    public UpdateNewsletterCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, ISenderRepository senderRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _senderRepository = senderRepository;
    }

    public override async Task<Unit> Handle(UpdateNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var newsletterData = await _senderRepository.GetNewsletter(request.Id);
        if (newsletterData is null) 
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        var emailCollection = await _senderRepository.GetNewsletter(newsletterData.Email);
        if (emailCollection is not null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        var updateNewsletter = new UpdateNewsletterDto
        {
            Email = request.Email ?? newsletterData.Email,
            Count = request.Count ?? newsletterData.Count,
            IsActivated = request.IsActivated ?? newsletterData.IsActivated,
            ModifiedBy = user?.UserId ?? Guid.Empty
        };

        await _senderRepository.UpdateNewsletter(updateNewsletter);
        return Unit.Value;
    }
}