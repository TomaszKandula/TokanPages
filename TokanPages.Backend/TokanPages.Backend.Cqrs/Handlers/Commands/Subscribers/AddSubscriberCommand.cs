namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

using System;
using MediatR;

public class AddSubscriberCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}