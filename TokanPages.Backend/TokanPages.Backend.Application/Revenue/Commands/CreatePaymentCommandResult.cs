using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class CreatePaymentCommandResult
{
    public Status? Status { get; set; }

    public string? RedirectUri { get; set; }
}
