namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;
    
public class ResetUserPasswordCommand : IRequest<Unit>
{
    public string? EmailAddress { get; set; }        
}