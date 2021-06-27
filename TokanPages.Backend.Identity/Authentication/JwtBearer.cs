using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TokanPages.Backend.Identity.Authorization;

namespace TokanPages.Backend.Identity.Authentication
{
	public class JwtBearer
    { 
	    public static void Configure(IServiceCollection AServices, IConfiguration AConfiguration)
        { 
	        var LIdentityServerAuthority = AConfiguration.GetValue<string>("IdentityServer:Authority");
	        var LRequireHttps = AConfiguration.GetValue<bool>("IdentityServer:RequireHttps");
	        var LAudience = AConfiguration.GetValue<string>("IdentityServer:Audience");
	        var LWebSecret = AConfiguration.GetValue<string>("IdentityServer:WebSecret");

	        AServices.AddAuthentication(AOption =>
	        {
		        AOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		        AOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	        }).AddJwtBearer(AOptions =>
	        {
		        AOptions.Authority = LIdentityServerAuthority;
		        AOptions.SaveToken = true;
		        AOptions.RequireHttpsMetadata = LRequireHttps;
		        AOptions.Audience = LAudience;
		        AOptions.TokenValidationParameters = new TokenValidationParameters
		        {
			        ValidateIssuerSigningKey = true,
			        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(LWebSecret)),
			        ValidateIssuer = true,
			        ValidateAudience = true,
			        ClockSkew = TimeSpan.Zero
		        };
		        AOptions.Events = new JwtBearerEvents
		        {
			        OnTokenValidated = ATokenValidatedContext =>
			        {
				        ValidateTokenClaims(ATokenValidatedContext);
				        return Task.CompletedTask;
			        }
		        };
	        });

	        AServices.AddAuthorization(AOptions =>
	        {
		        AOptions.AddPolicy("AuthPolicy", new AuthorizationPolicyBuilder()
				        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
				        .RequireAuthenticatedUser()
				        .Build());

		        AOptions.AddPolicy(Policies.AccessToTokanPages, APolicy => APolicy
			        .RequireClaim(Claims.Roles, Roles.GodOfAsgard, Roles.EverydayUser, 
				        Roles.ArticlePublisher, Roles.PhotoPublisher, Roles.CommentPublisher));
	        });
        }

	    private static void ValidateTokenClaims(TokenValidatedContext ATokenValidatedContext)
	    {
		    var LUserAlias = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.UserAlias) ?? Array.Empty<Claim>();
				        
		    var LRole = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.Roles) ?? Array.Empty<Claim>();
				        
		    var LAuthenticationType = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.AuthenticationType) ?? Array.Empty<Claim>();

		    var LUserId = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.UserId) ?? Array.Empty<Claim>();
				        
		    var LFirstName = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.FirstName) ?? Array.Empty<Claim>();
				        
		    var LLastName = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.LastName) ?? Array.Empty<Claim>();
				        
		    var LEmailAddress = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == Claims.EmailAddress) ?? Array.Empty<Claim>();

		    if (!LUserAlias.Any() || !LRole.Any() || !LAuthenticationType.Any() || !LUserId.Any()
		        || !LFirstName.Any() || !LLastName.Any() || !LEmailAddress.Any())
		    {
			    ATokenValidatedContext.Fail("Provided token is invalid.");
		    }
	    }
    }
}