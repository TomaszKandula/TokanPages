namespace TokanPages.Backend.Application.Users.Commands;

using MediatR;
    
public class ResetUserPasswordCommand : IRequest<Unit>
{
    public string? EmailAddress { get; set; }        
}