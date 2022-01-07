namespace TokanPages.Services.EmailSenderService;

using Models.Interfaces;

public interface IEmailSender
{
    Task SendNotification(IConfiguration configuration, CancellationToken cancellationToken = default);

    Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default);

    Task SendEmail(object content, CancellationToken cancellationToken = default);
}