using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Sender.Controllers.Mappers;
using TokanPages.Sender.Dto.Mailer;

namespace TokanPages.Sender.Controllers.Api;

/// <summary>
/// API endpoints definitions for mailer.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sender/[controller]/[action]")]
public class MailerController : ApiBaseController
{
    /// <summary>
    /// Mailer controller.
    /// </summary>
    /// <param name="mediator"></param>
    public MailerController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Sends message via email.
    /// </summary>
    /// <param name="payload">Message data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendMessage([FromBody] SendMessageDto payload)
        => await Mediator.Send(MailerMapper.MapToSendMessageCommand(payload));

    /// <summary>
    /// Sends newsletter message via email.
    /// </summary>
    /// <param name="payload">Message data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto payload)
        => await Mediator.Send(MailerMapper.MapToSendNewsletterCommand(payload));
}