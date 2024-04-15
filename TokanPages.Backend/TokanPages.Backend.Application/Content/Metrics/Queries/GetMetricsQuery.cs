using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetMetricsQuery : IRequest<IActionResult>
{
    public string Project { get; set; } = "";

    public string Metric { get; set; } = "";
}