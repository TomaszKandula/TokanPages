using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Persistence.DataAccess.Abstractions;
using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings) 
        : base(dbOperations, appSettings) { }

    /// <inheritdoc/>
    public async Task<Users.User?> GetUserById(Guid userId)
    {
        var filterBy = new { Id = userId };
        var data = await DbOperations.Retrieve<Users.User>(filterBy);
        return data.SingleOrDefault();
    }
}