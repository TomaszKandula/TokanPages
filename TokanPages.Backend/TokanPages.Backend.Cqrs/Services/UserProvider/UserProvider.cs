using System;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Cqrs.Services.UserProvider
{
    public class UserProvider : IUserProvider
    {
        private const string LOCALHOST = "127.0.0.1";
        
        private readonly IHttpContextAccessor FHttpContextAccessor;

        public UserProvider(IHttpContextAccessor AHttpContextAccessor) 
            => FHttpContextAccessor = AHttpContextAccessor;

        public UserProvider() { }

        public virtual string GetRequestIpAddress() 
        {
            var LRemoteIpAddress = FHttpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            return string.IsNullOrEmpty(LRemoteIpAddress) 
                ? LOCALHOST 
                : LRemoteIpAddress.Split(':')[0];
        }

        public virtual Guid? GetUserId() 
        {
            // TODO: return logged user id
            return null;
        }

        public virtual Users GetUserData() 
        {
            // TODO: return basic data
            return new ();
        }
    }
}
