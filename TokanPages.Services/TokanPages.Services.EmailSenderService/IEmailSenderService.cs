using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService;

public interface IEmailSenderService
{
    Task SendNotification(IEmailConfiguration configuration, CancellationToken cancellationToken = default);

    Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default);

    Task SendEmail(object content, CancellationToken cancellationToken = default);
}