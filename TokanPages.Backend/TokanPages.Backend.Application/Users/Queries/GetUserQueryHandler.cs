using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserQueryHandler : RequestHandler<GetUserQuery, GetUserQueryResult>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserRepository userRepository) 
        : base(operationDbContext, loggerService) => _userRepository = userRepository;

    public override async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userDetails = await _userRepository.GetUserDetails(request.Id);
        if (userDetails == null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return new GetUserQueryResult
        {
            Id = userDetails.UserId,
            Email = userDetails.EmailAddress,
            AliasName = userDetails.UserAlias,
            IsActivated = userDetails.IsActivated,
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName,
            Registered = userDetails.Registered,
            LastUpdated = userDetails.Modified
        };
    }
}