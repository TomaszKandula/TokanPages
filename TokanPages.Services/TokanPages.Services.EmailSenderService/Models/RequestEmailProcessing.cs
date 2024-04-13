namespace TokanPages.Services.EmailSenderService.Models;

public class RequestEmailProcessing
{
    public string? TargetEnv { get; set; }

    public CreateUserConfiguration? CreateUserConfiguration { get; set; }

    public ResetPasswordConfiguration? ResetPasswordConfiguration { get; set; }

    public VerifyEmailConfiguration? VerifyEmailConfiguration { get; set; }

    public SendMessageConfiguration? SendMessageConfiguration { get; set; }
}