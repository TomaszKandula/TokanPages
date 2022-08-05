namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;

public class RemoveUserMediaCommand : IRequest<Unit>
{
    public string UniqueBlobName { get; set; } = "";
}
