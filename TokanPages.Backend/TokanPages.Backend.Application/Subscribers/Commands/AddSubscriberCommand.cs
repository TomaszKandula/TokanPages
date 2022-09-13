using System;
using MediatR;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class AddSubscriberCommand : IRequest<Guid>
{
    public string? Email { get; set; }
}