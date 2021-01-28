using System;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Cqrs.Services.UserProvider
{
    public interface IUserProvider
    {
        string GetRequestIpAddress();
        Guid GetUserId();
        Users GetUserData();
    }
}
