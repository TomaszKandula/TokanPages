namespace TokanPages.Backend.Application.Assets.Commands;

using System;
using MediatR;

public class AddSingleAssetCommand : IRequest<AddSingleAssetCommandResult>
{
    public string MediaName { get; set; } = "";

    public string MediaType { get; set; } = "";

    public byte[] Data { get; set; } = Array.Empty<byte>();
}