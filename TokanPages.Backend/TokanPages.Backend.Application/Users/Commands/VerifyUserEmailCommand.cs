using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class VerifyUserEmailCommand : IRequest<Unit>
{
    public string LanguageId { get; set; } = "";

    public string EmailAddress { get; set; } = "";
}