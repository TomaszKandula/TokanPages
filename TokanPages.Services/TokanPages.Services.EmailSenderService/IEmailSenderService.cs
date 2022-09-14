using TokanPages.Services.EmailSenderService.Models.Interfaces;

namespace TokanPages.Services.EmailSenderService;

public interface IEmailSenderService
{
    Task SendNotification(IConfiguration configuration, CancellationToken cancellationToken = default);

    Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default);

    Task SendEmail(object content, CancellationToken cancellationToken = default);
}