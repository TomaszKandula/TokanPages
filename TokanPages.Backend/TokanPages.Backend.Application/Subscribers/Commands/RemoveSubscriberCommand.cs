namespace TokanPages.Backend.Application.Handlers.Commands.Subscribers;

using System;
using MediatR;

public class RemoveSubscriberCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}