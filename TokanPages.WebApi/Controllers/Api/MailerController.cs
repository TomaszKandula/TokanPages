using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.WebApi.Controllers.Mappers;
using TokanPages.WebApi.Dto.Mailer;

namespace TokanPages.WebApi.Controllers.Api;

/// <summary>
/// API endpoints definitions for mailer
/// </summary>
[Authorize]
[ApiVersion("1.0")]
public class MailerController : ApiBaseController
{
    /// <summary>
    /// Mailer controller
    /// </summary>
    /// <param name="mediator"></param>
    public MailerController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Sends message via email
    /// </summary>
    /// <param name="payLoad">Message data</param>
    /// <returns>MediatR unit value</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendMessage([FromBody] SendMessageDto payLoad)
        => await Mediator.Send(MailerMapper.MapToSendMessageCommand(payLoad));

    /// <summary>
    /// Sends newsletter message via email
    /// </summary>
    /// <param name="payLoad">Message data</param>
    /// <returns>MediatR unit value</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto payLoad)
        => await Mediator.Send(MailerMapper.MapToSendNewsletterCommand(payLoad));
}