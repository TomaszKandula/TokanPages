using MediatR;

namespace TokanPages.Backend.Application.Users.Commands;

public class ResetUserPasswordCommand : IRequest<Unit>
{
    public string? EmailAddress { get; set; }        
}