namespace TokanPages.Backend.Cqrs.Handlers.Commands.Assets;

using System;
using MediatR;

public class AddSingleAssetCommand : IRequest<AddSingleAssetCommandResult>
{
    public string MediaName { get; set; } = "";

    public string MediaType { get; set; } = "";

    public byte[] Data { get; set; } = Array.Empty<byte>();
}