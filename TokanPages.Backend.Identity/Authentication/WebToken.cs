namespace TokanPages.Backend.Identity.Authentication
{
	using System;
	using System.Text;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using System.Diagnostics.CodeAnalysis;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Authorization;

	[ExcludeFromCodeCoverage]
	public static class WebToken
    { 
	    public static void Configure(IServiceCollection AServices, IConfiguration AConfiguration)
        { 
	        var LIssuer = AConfiguration.GetValue<string>("IdentityServer:Issuer");
	        var LAudience = AConfiguration.GetValue<string>("IdentityServer:Audience");
	        var LWebSecret = AConfiguration.GetValue<string>("IdentityServer:WebSecret");
	        var LRequireHttps = AConfiguration.GetValue<bool>("IdentityServer:RequireHttps");

	        AServices.AddAuthentication(AOption =>
	        {
		        AOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		        AOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	        }).AddJwtBearer(AOptions =>
	        {
		        AOptions.Audience = LAudience;
		        AOptions.SecurityTokenValidators.Clear();
		        AOptions.SecurityTokenValidators.Add(new SecurityHandler());
		        AOptions.SaveToken = true;
		        AOptions.RequireHttpsMetadata = LRequireHttps;
		        AOptions.TokenValidationParameters = new TokenValidationParameters
		        {
			        ValidateIssuerSigningKey = true,
			        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(LWebSecret)),
			        ValidateIssuer = true,
			        ValidIssuer = LIssuer,
			        ValidateAudience = true,
			        ValidAudience = LAudience,
			        ValidateLifetime = true,
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

		        AOptions.AddPolicy(nameof(Policies.AccessToTokanPages), APolicy => APolicy
			        .RequireRole(nameof(Roles.GodOfAsgard), nameof(Roles.EverydayUser), nameof(Roles.ArticlePublisher), 
				        nameof(Roles.PhotoPublisher), nameof(Roles.CommentPublisher)));
	        });
        }

	    private static void ValidateTokenClaims(TokenValidatedContext ATokenValidatedContext)
	    {
		    var LUserAlias = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.Name) ?? Array.Empty<Claim>();
				        
		    var LRole = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.Role) ?? Array.Empty<Claim>();
				        
		    var LUserId = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.NameIdentifier) ?? Array.Empty<Claim>();
				        
		    var LFirstName = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.GivenName) ?? Array.Empty<Claim>();
				        
		    var LLastName = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.Surname) ?? Array.Empty<Claim>();
				        
		    var LEmailAddress = ATokenValidatedContext.Principal?.Claims
			    .Where(AClaim => AClaim.Type == ClaimTypes.Email) ?? Array.Empty<Claim>();

		    if (!LUserAlias.Any() || !LRole.Any() || !LUserId.Any()
		        || !LFirstName.Any() || !LLastName.Any() || !LEmailAddress.Any())
		    {
			    ATokenValidatedContext.Fail("Provided token is invalid.");
		    }
	    }
    }
}