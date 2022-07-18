namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using Domain.Enums;
using MediatR;

public class UploadUserMediaCommand : IRequest<UploadUserMediaCommandResult>
{
    public Guid? UserId { get; set; }

    public UserMedia MediaTarget { get; set; }

    public string MediaName { get; set; } = "";

    public string MediaType { get; set; } = "";

    public byte[] Data { get; set; } = Array.Empty<byte>();
}