using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Configuration;

public static class WebTokenSupport
{
	public static void SetupWebToken(IServiceCollection services, IConfiguration configuration)
	{ 
		var issuer = configuration.GetValue<string>("Ids_Issuer");
		var audience = configuration.GetValue<string>("Ids_Audience");
		var webSecret = configuration.GetValue<string>("Ids_WebSecret");
		var requireHttps = configuration.GetValue<bool>("Ids_RequireHttps");

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
				OnTokenValidated = async context =>
				{
					await ValidateTokenClaims(context);
				},
				OnForbidden = context =>
				{
					context.Fail(ErrorCodes.ACCESS_DENIED);
					return Task.FromException(ReturnAccessDenied());
				},
				OnAuthenticationFailed = context =>
				{
					context.Fail(ErrorCodes.INVALID_USER_TOKEN);
					return Task.FromException(ReturnInvalidToken());
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

	private static Task ValidateTokenClaims(TokenValidatedContext context)
	{
		var userAlias = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Name) ?? Array.Empty<Claim>();
				        
		var role = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Role) ?? Array.Empty<Claim>();
				        
		var userId = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.NameIdentifier) ?? Array.Empty<Claim>();
				        
		var firstName = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.GivenName) ?? Array.Empty<Claim>();
				        
		var lastName = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Surname) ?? Array.Empty<Claim>();
				        
		var emailAddress = context.Principal?.Claims
			.Where(claim => claim.Type == ClaimTypes.Email) ?? Array.Empty<Claim>();

		if (userAlias.Any() && role.Any() && userId.Any() && firstName.Any() && lastName.Any() && emailAddress.Any())
			return Task.CompletedTask;

		context.Fail("Provided token is invalid.");
		return Task.FromException(ReturnInvalidClaims());
	}

	private static AccessException ReturnAccessDenied() 
		=> new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

	private static AuthorizationException ReturnInvalidToken() 
		=> new (nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

	private static AuthorizationException ReturnInvalidClaims() 
		=> new (nameof(ErrorCodes.INVALID_TOKEN_CLAIMS), ErrorCodes.INVALID_TOKEN_CLAIMS);
}