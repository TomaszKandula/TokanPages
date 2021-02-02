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

        public UserProvider() 
        { 
        }

        public virtual string GetRequestIpAddress() 
        {
            var LRemoteIpAddress = FHttpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(LRemoteIpAddress))
            {
                return "127.0.0.1";
            }

            return LRemoteIpAddress;
        }

        public virtual Guid? GetUserId() 
        {
            // TODO: return logged user id
            return null;
        }

        public virtual Users GetUserData() 
        {
            // TODO: return basic data
            return new Users();
        }
    }
}
