using System;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Cqrs.Services.UserProvider
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor FHttpContextAccessor;

        public UserProvider(IHttpContextAccessor AHttpContextAccessor) 
        {
            FHttpContextAccessor = AHttpContextAccessor;
        }

        public string GetRequestIpAddress() 
        {
            return FHttpContextAccessor
                .HttpContext
                .Connection
                .RemoteIpAddress
                .MapToIPv4()
                .ToString();
        }

        public Guid GetUserId() 
        {
            // TODO: return logged user id
            return Guid.Empty;
        }

        public Users GetUserData() 
        {
            // TODO: return basic data
            return new Users();
        }
    }
}
