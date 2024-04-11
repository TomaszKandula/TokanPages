namespace TokanPages.Services.EmailSenderService.Abstractions;

public interface IEmailConfiguration
{
    public Guid MessageId { get; set; }
}