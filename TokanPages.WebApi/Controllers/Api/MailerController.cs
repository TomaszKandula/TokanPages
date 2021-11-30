namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Attributes;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Mailer;
    using Backend.Identity.Authorization;
    using MediatR;

    [Authorize]
    public class MailerController : ApiBaseController
    {
        public MailerController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> SendMessage([FromBody] SendMessageDto payLoad)
            => await Mediator.Send(MailerMapper.MapToSendMessageCommand(payLoad));

        [HttpPost]
        [AuthorizeUser(Roles.GodOfAsgard)]
        public async Task<Unit> SendNewsletter([FromBody] SendNewsletterDto payLoad)
            => await Mediator.Send(MailerMapper.MapToSendNewsletterCommand(payLoad));
    }
}