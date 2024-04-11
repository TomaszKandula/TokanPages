namespace TokanPages.Services.EmailSenderService.Abstractions;

public interface IEmailSenderService
{
    Task SendNotification(IEmailConfiguration configuration, CancellationToken cancellationToken = default);

    Task<string> GetEmailTemplate(string templateUrl, CancellationToken cancellationToken = default);

    Task SendEmail(object content, CancellationToken cancellationToken = default);

    Task SendToServiceBus(IEmailConfiguration configuration, CancellationToken cancellationToken = default);
}