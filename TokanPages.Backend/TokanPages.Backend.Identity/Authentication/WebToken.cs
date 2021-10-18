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
	    public static void Configure(IServiceCollection services, IConfiguration configuration)
        { 
	        var issuer = configuration.GetValue<string>("IdentityServer:Issuer");
	        var audience = configuration.GetValue<string>("IdentityServer:Audience");
	        var webSecret = configuration.GetValue<string>("IdentityServer:WebSecret");
	        var requireHttps = configuration.GetValue<bool>("IdentityServer:RequireHttps");

	        services.AddAuthentication(options =>
	        {
		        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	        }).AddJwtBearer(options =>
	        {
		        options.Audience = audience;
		        options.SecurityTokenValidators.Clear();
		        options.SecurityTokenValidators.Add(new SecurityHandler());
		        options.SaveToken = true;
		        options.RequireHttpsMetadata = requireHttps;
		        options.TokenValidationParameters = new TokenValidationParameters
		        {
			        ValidateIssuerSigningKey = true,
			        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(webSecret)),
			        ValidateIssuer = true,
			        ValidIssuer = issuer,
			        ValidateAudience = true,
			        ValidAudience = audience,
			        ValidateLifetime = true,
			        ClockSkew = TimeSpan.Zero
		        };
		        options.Events = new JwtBearerEvents
		        {
			        OnTokenValidated = tokenValidatedContext =>
			        {
				        ValidateTokenClaims(tokenValidatedContext);
				        return Task.CompletedTask;
			        }
		        };
	        });

	        services.AddAuthorization(options =>
	        {
		        options.AddPolicy("AuthPolicy", new AuthorizationPolicyBuilder()
				        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
				        .RequireAuthenticatedUser()
				        .Build());

		        options.AddPolicy(nameof(Policies.AccessToTokanPages), policy => policy
			        .RequireRole(nameof(Roles.GodOfAsgard), nameof(Roles.EverydayUser), nameof(Roles.ArticlePublisher), 
				        nameof(Roles.PhotoPublisher), nameof(Roles.CommentPublisher)));
	        });
        }

	    private static void ValidateTokenClaims(TokenValidatedContext tokenValidatedContext)
	    {
		    var userAlias = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.Name) ?? Array.Empty<Claim>();
				        
		    var role = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.Role) ?? Array.Empty<Claim>();
				        
		    var userId = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.NameIdentifier) ?? Array.Empty<Claim>();
				        
		    var firstName = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.GivenName) ?? Array.Empty<Claim>();
				        
		    var lastName = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.Surname) ?? Array.Empty<Claim>();
				        
		    var emailAddress = tokenValidatedContext.Principal?.Claims
			    .Where(claim => claim.Type == ClaimTypes.Email) ?? Array.Empty<Claim>();

		    if (!userAlias.Any() || !role.Any() || !userId.Any()
		        || !firstName.Any() || !lastName.Any() || !emailAddress.Any())
		    {
			    tokenValidatedContext.Fail("Provided token is invalid.");
		    }
	    }
    }
}