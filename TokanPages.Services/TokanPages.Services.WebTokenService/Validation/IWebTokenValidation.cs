namespace TokanPages.Services.WebTokenService.Validation;

public interface IWebTokenValidation
{
    string GetWebTokenFromHeader();

    Task VerifyUserToken();    
}