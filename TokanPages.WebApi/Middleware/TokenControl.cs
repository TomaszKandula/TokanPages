namespace TokanPages.WebApi.Middleware
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Features;
    using Backend.Cqrs.Services.UserServiceProvider;

    [ExcludeFromCodeCoverage]
    public class TokenControl
    {
        private readonly RequestDelegate _requestDelegate;

        public TokenControl(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        /// <summary>
        /// Suppose JWT is present in the request header and passes a formal check.
        /// In that case, we verify whether it is already revoked or not in the database.
        /// We skip all the endpoints that do not require authorization.
        /// </summary>
        /// <remarks>
        /// We implicitly exclude swagger UI. 
        /// </remarks>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="userServiceProvider">User service provider instance.</param>
        public async Task InvokeAsync(HttpContext httpContext, IUserServiceProvider userServiceProvider)
        {
            var isSwagger = httpContext.Request.Path.Value?.Contains("/swagger/") ?? false;
            if (isSwagger)
            {
                await _requestDelegate(httpContext);
                return;
            }

            var endpoint = httpContext.Features.Get<IEndpointFeature>().Endpoint;
            var hasAllowAnonymousAttribute = endpoint != null && endpoint.Metadata.Any(@object => @object is AllowAnonymousAttribute);
            if (hasAllowAnonymousAttribute)
            {
                await _requestDelegate(httpContext);
                return;
            }

            await userServiceProvider.VerifyUserToken();
            await _requestDelegate(httpContext);
        }
    }
}