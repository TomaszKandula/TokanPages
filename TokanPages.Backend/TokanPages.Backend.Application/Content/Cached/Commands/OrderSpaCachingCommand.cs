using MediatR;
using TokanPages.Backend.Application.Content.Cached.Commands.Models;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class OrderSpaCachingCommand : IRequest<Unit>
{
    public string GetUrl { get; set; } = "";

    public string PostUrl { get; set; } = "";

    public string[] Files { get; set; } = Array.Empty<string>();

    public List<RoutePath> Paths { get; set; } = new();
}