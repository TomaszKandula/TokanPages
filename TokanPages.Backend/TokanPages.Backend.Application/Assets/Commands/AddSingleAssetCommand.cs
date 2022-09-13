using System;
using MediatR;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddSingleAssetCommand : IRequest<AddSingleAssetCommandResult>
{
    public string MediaName { get; set; } = "";

    public string MediaType { get; set; } = "";

    public byte[] Data { get; set; } = Array.Empty<byte>();
}