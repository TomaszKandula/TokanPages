namespace TokanPages.Backend.Application.Subscribers.Commands;

using System;
using MediatR;

public class RemoveSubscriberCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}