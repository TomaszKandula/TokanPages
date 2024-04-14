using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Metrics.Queries;

public class GetQualityGateQuery : IRequest<IActionResult>
{
    public string Project { get; set; } = "";
}