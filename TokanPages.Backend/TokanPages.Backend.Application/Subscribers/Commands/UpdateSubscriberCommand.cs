namespace TokanPages.Backend.Application.Subscribers.Commands;

using System;
using MediatR;

public class UpdateSubscriberCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public string? Email { get; set; }
        
    public bool? IsActivated { get; set; }
        
    public int? Count { get; set; }
}