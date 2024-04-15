using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Content.Metrics.Queries;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for sonar qube metrics.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class MetricsController : ApiBaseController
{
    /// <summary>
    /// Metrics controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    public MetricsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns badge from SonarQube server for given project name and metric type.
    /// All badges have the same style.
    /// </summary>
    /// <param name="project">SonarQube analysis project name.</param>
    /// <param name="metric">SonarQube metric type.</param>
    /// <returns>SonarQube badge.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMetrics([FromQuery] string project, string metric) 
        => await Mediator.Send(new GetMetricsQuery { Project = project, Metric = metric });

    /// <summary>
    /// Returns large quality gate badge from SonarQube server for given project name.
    /// </summary>
    /// <param name="project">SonarQube analysis project name.</param>
    /// <returns>SonarQube badge.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQualityGate([FromQuery] string project) 
        => await Mediator.Send(new GetQualityGateQuery { Project = project });
}