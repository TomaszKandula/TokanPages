using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public class UserRepository : RepositoryBase, IUserRepository
{
    private readonly IDateTimeService _dateTimeService;

    public UserRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    /// <inheritdoc/>
    public async Task<Users.User?> GetUserById(Guid userId)
    {
        var filterBy = new { Id = userId };
        var data = await DbOperations.Retrieve<Users.User>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<Users.UserInfo?> GetUserInformationById(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<Users.UserInfo>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task InsertHttpRequest(string ipAddress, string handlerName)
    {
        var entity = new HttpRequest
        {
            Id = Guid.NewGuid(),
            SourceAddress = ipAddress,
            RequestedAt = _dateTimeService.Now,
            RequestedHandlerName = handlerName
        };

        await DbOperations.Insert(entity);
    }
}