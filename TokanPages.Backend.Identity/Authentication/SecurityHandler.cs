namespace TokanPages.Backend.Identity.Authentication
{
    using System.Security.Claims;
    using System.Diagnostics.CodeAnalysis;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;

    [ExcludeFromCodeCoverage]
    public class SecurityHandler : ISecurityTokenValidator
    {
        private readonly JwtSecurityTokenHandler FTokenHandler;

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public SecurityHandler() => FTokenHandler = new JwtSecurityTokenHandler();

        public bool CanReadToken(string ASecurityToken) => FTokenHandler.CanReadToken(ASecurityToken);

        public ClaimsPrincipal ValidateToken(string ASecurityToken, TokenValidationParameters AValidationParameters,
            out SecurityToken AValidatedToken)
        {
            //TODO: extend custom validation
            var LPrincipal = FTokenHandler.ValidateToken(ASecurityToken, AValidationParameters, out AValidatedToken);
            return LPrincipal;
        }
    }
}