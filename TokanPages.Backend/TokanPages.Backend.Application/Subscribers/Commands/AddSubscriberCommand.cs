namespace TokanPages.Backend.Application.Subscribers.Commands;

using System;
using MediatR;

public class AddSubscriberCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}