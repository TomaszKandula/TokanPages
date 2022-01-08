namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;
using Backend.Cqrs.Mappers;
using Backend.Shared.Attributes;
using Backend.Shared.Dto.Mailer;
using MediatR;

[Authorize]
[ApiVersion("1.0")]
public class MailerController : ApiBaseController
{
    public MailerController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendMessage([FromBody] SendMessageDto payLoad)
        => await Mediator.Send(MailerMapper.MapToSendMessageCommand(payLoad));

    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto payLoad)
        => await Mediator.Send(MailerMapper.MapToSendNewsletterCommand(payLoad));
}