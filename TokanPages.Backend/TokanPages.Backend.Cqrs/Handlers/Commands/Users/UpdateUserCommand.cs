#nullable enable
namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class UpdateUserCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }

    public bool IsActivated { get; set; }

    public string? UserAlias { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? ShortBio { get; set; }
}