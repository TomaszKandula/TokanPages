namespace TokanPages.Services.WebTokenService.Abstractions;

public interface IWebTokenValidation
{
    string GetWebTokenFromHeader();

    Task VerifyUserToken();    
}