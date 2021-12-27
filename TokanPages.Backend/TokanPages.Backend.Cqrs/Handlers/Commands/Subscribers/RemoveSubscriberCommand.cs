namespace TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

using System;
using MediatR;

public class RemoveSubscriberCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}